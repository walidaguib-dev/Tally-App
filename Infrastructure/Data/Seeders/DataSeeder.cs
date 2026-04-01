using Bogus;
using Domain.Entities;
using Domain.Entities.Teams;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Seeders
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // ─── 1. ROLES ───────────────────────────────────────────────────
            string[] roles = ["Chef", "Tallyman"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ─── 2. USERS ───────────────────────────────────────────────────
            var createdUserIds = new List<string>();

            if (!context.Users.Any())
            {
                var userFaker = new Faker();

                // Admin / Chef
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@tally.com",
                    EmailConfirmed = true,
                    PhoneNumber = userFaker.Phone.PhoneNumber(),
                };
                var adminResult = await userManager.CreateAsync(admin, "Admin@123456");
                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Chef");
                    createdUserIds.Add(admin.Id);
                }

                // Tallymen
                for (int i = 1; i <= 8; i++)
                {
                    var tallyman = new User
                    {
                        UserName = $"tallyman{i}",
                        Email = $"tallyman{i}@tally.com",
                        EmailConfirmed = true,
                        PhoneNumber = userFaker.Phone.PhoneNumber(),
                    };
                    var result = await userManager.CreateAsync(tallyman, "Tallyman@123456");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(tallyman, "Tallyman");
                        createdUserIds.Add(tallyman.Id);
                    }
                }
            }
            else
            {
                createdUserIds = await context.Users.Select(x => x.Id).ToListAsync();
            }

            // ─── 3. USER PROFILES ───────────────────────────────────────────
            if (!context.profiles.Any())
            {
                var profileFaker = new Faker<UserProfile>()
                    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                    .RuleFor(x => x.LastName, f => f.Name.LastName())
                    .RuleFor(x => x.Bio, f => f.Lorem.Sentence())
                    .RuleFor(x => x.CreatedAt, f => f.Date.Recent(60).ToUniversalTime());

                var profiles = createdUserIds
                    .Select(userId =>
                    {
                        var profile = profileFaker.Generate();
                        profile.UserId = userId;
                        return profile;
                    })
                    .ToList();

                context.profiles.AddRange(profiles);
                await context.SaveChangesAsync();
            }

            // ─── 4. SHIPS ───────────────────────────────────────────────────
            if (!context.Ships.Any())
            {
                var ships = new List<Ship>
                {
                    new() { Name = "MSC Ambra", ImoNumber = "9234567" },
                    new() { Name = "CMA CGM Tanger", ImoNumber = "9345678" },
                    new() { Name = "OOCL Hamburg", ImoNumber = "9456789" },
                    new() { Name = "Maersk Djibouti", ImoNumber = "9567890" },
                    new() { Name = "Evergreen Marine", ImoNumber = "9678901" },
                    new() { Name = "Hapag-Lloyd Algiers", ImoNumber = "9789012" },
                    new() { Name = "Cosco Annaba", ImoNumber = "9890123" },
                    new() { Name = "Yang Ming Jijel", ImoNumber = "9901234" },
                };
                context.Ships.AddRange(ships);
                await context.SaveChangesAsync();
            }

            var shipIds = await context.Ships.Select(x => x.Id).ToListAsync();

            // ─── 5. TRUCKS ──────────────────────────────────────────────────
            if (!context.Trucks.Any())
            {
                var truckFaker = new Faker<Truck>()
                    .RuleFor(x => x.PlateNumber, f => f.Random.Replace("##-#####"))
                    .RuleFor(x => x.Capacity, f => Math.Round(f.Random.Double(5, 35), 1));

                context.Trucks.AddRange(truckFaker.Generate(20));
                await context.SaveChangesAsync();
            }

            var truckIds = await context.Trucks.Select(x => x.Id).ToListAsync();

            // ─── 6. MERCHANDISES ────────────────────────────────────────────
            if (!context.Merchandises.Any())
            {
                var merchandises = new List<Merchandise>
                {
                    new() { Name = "Cement", Type = "bulk" },
                    new() { Name = "Iron Bars", Type = "bulk" },
                    new() { Name = "Wheat", Type = "bagged" },
                    new() { Name = "Sugar", Type = "bagged" },
                    new() { Name = "Fertilizer", Type = "bagged" },
                    new() { Name = "Cooking Oil", Type = "liquid" },
                    new() { Name = "Cotton", Type = "baled" },
                    new() { Name = "Timber", Type = "bulk" },
                };
                context.Merchandises.AddRange(merchandises);
                await context.SaveChangesAsync();
            }

            var merchandiseIds = await context.Merchandises.Select(x => x.Id).ToListAsync();

            // ─── 7. CLIENTS ─────────────────────────────────────────────────
            if (!context.Clients.Any())
            {
                var clientFaker = new Faker<Client>()
                    .RuleFor(x => x.Name, f => f.Company.CompanyName())
                    .RuleFor(x => x.ContactInfo, f => f.Phone.PhoneNumber())
                    .RuleFor(x => x.MerchandiseId, f => f.PickRandom(merchandiseIds))
                    .RuleFor(
                        x => x.Bill_Of_Lading,
                        f =>
                            f.Make(f.Random.Int(1, 3), () => $"DJD{f.Random.Number(1000, 9999)}")
                                .ToList()
                    );

                context.Clients.AddRange(clientFaker.Generate(15));
                await context.SaveChangesAsync();
            }

            var clientIds = await context.Clients.Select(x => x.Id).ToListAsync();

            // ─── 8. TEAMS ───────────────────────────────────────────────────
            if (!context.Teams.Any())
            {
                var supervisorId = createdUserIds.FirstOrDefault()!;

                var teams = new List<Team>
                {
                    new()
                    {
                        Name = "Group A",
                        Description = "Morning shift team",
                        SupervisorId = supervisorId,
                    },
                    new()
                    {
                        Name = "Group B",
                        Description = "Afternoon shift team",
                        SupervisorId = supervisorId,
                    },
                    new()
                    {
                        Name = "Group C",
                        Description = "Night shift team",
                        SupervisorId = supervisorId,
                    },
                };
                context.Teams.AddRange(teams);
                await context.SaveChangesAsync();
            }

            var teamIds = await context.Teams.Select(x => x.Id).ToListAsync();

            // ─── 9. TEAM MEMBERS ────────────────────────────────────────────
            if (!context.TeamMembers.Any())
            {
                var tallymanIds = createdUserIds.Skip(2).ToList(); // skip admin and supervisor
                var random = new Random();
                var members = new List<TeamMember>();

                foreach (var userId in tallymanIds)
                {
                    members.Add(
                        new TeamMember
                        {
                            UserId = userId,
                            TeamId = teamIds[random.Next(teamIds.Count)],
                            Role = "tallyman",
                            JoinedAt = DateTime.UtcNow.AddDays(-random.Next(1, 90)),
                        }
                    );
                }
                context.TeamMembers.AddRange(members);
                await context.SaveChangesAsync();
            }

            // ─── 10. TALLY SHEETS ───────────────────────────────────────────
            if (!context.TallySheets.Any())
            {
                var faker = new Faker();
                var tallySheets = new List<TallySheet>();

                for (int i = 0; i < 20; i++)
                {
                    tallySheets.Add(
                        new TallySheet
                        {
                            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-i)),
                            TeamsCount = faker.Random.Int(1, 5),
                            Shift = faker.PickRandom<ShiftType>(),
                            Zone = faker.PickRandom<ZoneType>(),
                            ShipId = faker.PickRandom(shipIds),
                            UserId = faker.PickRandom(createdUserIds),
                        }
                    );
                }

                context.TallySheets.AddRange(tallySheets);
                await context.SaveChangesAsync();
            }

            var tallySheetIds = await context.TallySheets.Select(x => x.Id).ToListAsync();

            // ─── 11. TALLY SHEET TRUCKS ─────────────────────────────────────
            if (!context.TallySheetTrucks.Any())
            {
                var faker = new Faker();
                var sessions = new List<TallySheetTruck>();
                var usedPairs = new HashSet<(int, int)>();

                foreach (var tallySheetId in tallySheetIds.Take(10))
                {
                    int count = faker.Random.Int(1, 3);
                    var shuffledTrucks = truckIds.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();

                    foreach (var truckId in shuffledTrucks)
                    {
                        if (usedPairs.Add((tallySheetId, truckId)))
                        {
                            var start = TimeOnly.FromTimeSpan(
                                TimeSpan.FromHours(faker.Random.Int(6, 14))
                            );
                            sessions.Add(
                                new TallySheetTruck
                                {
                                    TallySheetId = tallySheetId,
                                    TruckId = truckId,
                                    StartTime = start,
                                    EndTime = start.AddHours(faker.Random.Int(1, 4)),
                                }
                            );
                        }
                    }
                }

                context.TallySheetTrucks.AddRange(sessions);
                await context.SaveChangesAsync();
            }

            // ─── 12. PAUSES ─────────────────────────────────────────────────
            if (!context.Pauses.Any())
            {
                var faker = new Faker();
                var pauses = new List<Pause>();

                foreach (var tallySheetId in tallySheetIds.Take(8))
                {
                    pauses.Add(
                        new Pause
                        {
                            TallySheetId = tallySheetId,
                            Reason = faker.PickRandom<PauseReason>(),
                            StartTime = TimeOnly.FromTimeSpan(
                                TimeSpan.FromHours(faker.Random.Int(8, 12))
                            ),
                            EndTime = TimeOnly.FromTimeSpan(
                                TimeSpan.FromHours(faker.Random.Int(13, 16))
                            ),
                            Notes = faker.Lorem.Sentence(),
                            TruckId = faker.Random.Bool() ? faker.PickRandom(truckIds) : null,
                        }
                    );
                }

                context.Pauses.AddRange(pauses);
                await context.SaveChangesAsync();
            }

            // ─── 13. TALLY SHEET CLIENTS ────────────────────────────────────
            if (!context.TallySheetClients.Any())
            {
                var faker = new Faker();
                var tsClients = new List<TallySheetClient>();
                var usedPairs = new HashSet<(int, int)>();

                foreach (var tallySheetId in tallySheetIds.Take(10))
                {
                    int count = faker.Random.Int(1, 3);
                    var shuffledClients = clientIds
                        .OrderBy(_ => Guid.NewGuid())
                        .Take(count)
                        .ToList();

                    foreach (var clientId in shuffledClients)
                    {
                        if (usedPairs.Add((tallySheetId, clientId)))
                        {
                            tsClients.Add(
                                new TallySheetClient
                                {
                                    TallySheetId = tallySheetId,
                                    ClientId = clientId,
                                    Quantity = faker.Random.Int(10, 500),
                                    Unit = faker.PickRandom("bags", "pieces", "tons", "packages"),
                                    Notes = faker.Lorem.Sentence(),
                                    LastUpdated = DateTime.UtcNow.AddHours(
                                        -faker.Random.Int(1, 48)
                                    ),
                                }
                            );
                        }
                    }
                }

                context.TallySheetClients.AddRange(tsClients);
                await context.SaveChangesAsync();
            }

            // ─── 14. CONTAINERS ─────────────────────────────────────────────
            if (!context.Containers.Any())
            {
                var faker = new Faker();
                var ownerCodes = new[] { "MSCU", "CMAU", "OOLU", "HLCU", "EVGU" };
                var containers = new List<Container>();

                for (int i = 0; i < 30; i++)
                {
                    var owner = faker.PickRandom(ownerCodes);
                    var digits = faker.Random.Replace("######");
                    var check = faker.Random.Int(0, 9).ToString();

                    containers.Add(
                        new Container
                        {
                            ContainerNumber = $"{owner}{digits}{check}",
                            Bill_of_lading = $"DJD{faker.Random.Number(1000, 9999)}",
                            ContainerSize = faker.PickRandom<ContainerSize>(),
                            ContainerType = faker.PickRandom<ContainerType>(),
                            ContainerStatus = faker.PickRandom<ContainerStatus>(),
                            SealNumber = faker.Random.Replace("######"),
                            userId = faker.PickRandom(createdUserIds),
                            TallySheetId = faker.PickRandom(tallySheetIds),
                            ClientId = faker.PickRandom(clientIds),
                        }
                    );
                }

                context.Containers.AddRange(containers);
                await context.SaveChangesAsync();
            }

            // ─── 15. CARS ───────────────────────────────────────────────────
            if (!context.Cars.Any())
            {
                var faker = new Faker();
                var cars = new List<Cars>();

                var brands = new[]
                {
                    "Toyota",
                    "Mercedes",
                    "Renault",
                    "Peugeot",
                    "Ford",
                    "Volkswagen",
                };
                var types = new[] { "Sedan", "SUV", "Van", "Truck", "Bus" };

                for (int i = 0; i < 20; i++)
                {
                    cars.Add(
                        new Cars
                        {
                            Brand = faker.PickRandom(brands),
                            Type = faker.PickRandom(types),
                            VinNumber = faker.Random.Replace("######"),
                            Bill_Of_Lading = $"DJD{faker.Random.Number(1000, 9999)}",
                            carStatus = faker.PickRandom<CarStatus>(),
                            UserId = faker.PickRandom(createdUserIds),
                            TallySheetId = faker.PickRandom(tallySheetIds),
                            ShipId = faker.PickRandom(shipIds),
                            RecordedAt = faker.Date.Recent(30).ToUniversalTime(),
                        }
                    );
                }

                context.Cars.AddRange(cars);
                await context.SaveChangesAsync();
            }

            // ─── 16. OBSERVATIONS ───────────────────────────────────────────
            if (!context.Observations.Any())
            {
                var faker = new Faker();
                var observations = new List<Observation>();

                foreach (var tallySheetId in tallySheetIds.Take(10))
                {
                    int count = faker.Random.Int(1, 3);
                    for (int i = 0; i < count; i++)
                    {
                        observations.Add(
                            new Observation
                            {
                                TallySheetId = tallySheetId,
                                Type = faker.PickRandom<ObservationType>(),
                                Description = faker.Lorem.Sentence(),
                                Timestamp = TimeOnly.FromDateTime(DateTime.UtcNow),
                                ClientId = faker.Random.Bool() ? faker.PickRandom(clientIds) : null,
                                TruckId = faker.Random.Bool() ? faker.PickRandom(truckIds) : null,
                            }
                        );
                    }
                }

                context.Observations.AddRange(observations);
                await context.SaveChangesAsync();
            }
        }
    }
}
