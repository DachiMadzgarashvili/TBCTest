using TBCTest.Data;
using TBCTest.Models;

namespace TBCTest.Services
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Seed cities
            if (!context.Cities.Any())
            {
                var cities = new List<City>
                {
                    new City { NameGe = "თბილისი", NameEn = "Tbilisi" },
                    new City { NameGe = "ბათუმი", NameEn = "Batumi" },
                    new City { NameGe = "ქუთაისი", NameEn = "Kutaisi" },
                    new City { NameGe = "რუსთავი", NameEn = "Rustavi" },
                    new City { NameGe = "ზუგდიდი", NameEn = "Zugdidi" },
                    new City { NameGe = "გორი", NameEn = "Gori" },
                    new City { NameGe = "ფოთი", NameEn = "Poti" },
                    new City { NameGe = "თელავი", NameEn = "Telavi" },
                    new City { NameGe = "სამტრედია", NameEn = "Samtredia" },
                    new City { NameGe = "მარნეული", NameEn = "Marneuli" },
                    new City { NameGe = "მცხეთა", NameEn = "Mtskheta" },
                    new City { NameGe = "გარდაბანი", NameEn = "Gardabani" },
                    new City { NameGe = "ბოლნისი", NameEn = "Bolnisi" },
                    new City { NameGe = "ხარაგაული", NameEn = "Kharagauli" },
                    new City { NameGe = "წალკა", NameEn = "Tsalka" },
                    new City { NameGe = "სენაკი", NameEn = "Senaki" },
                    new City { NameGe = "ხონი", NameEn = "Khoni" },
                    new City { NameGe = "ლანჩხუთი", NameEn = "Lanchkhuti" },
                    new City { NameGe = "ჭიათურა", NameEn = "Chiatura" },
                    new City { NameGe = "საჩხერე", NameEn = "Sachkhere" },
                    new City { NameGe = "წყალტუბო", NameEn = "Tskaltubo" },
                    new City { NameGe = "ხაშური", NameEn = "Khashuri" },
                    new City { NameGe = "გურჯაანი", NameEn = "Gurjaani" },
                    new City { NameGe = "ბორჯომი", NameEn = "Borjomi" },
                    new City { NameGe = "ოზურგეთი", NameEn = "Ozurgeti" },
                    new City { NameGe = "ქობულეთი", NameEn = "Kobuleti" },
                    new City { NameGe = "ამბროლაური", NameEn = "Ambrolauri" },
                    new City { NameGe = "ჯვარი", NameEn = "Jvari" }
                };
                context.Cities.AddRange(cities);
                await context.SaveChangesAsync();
            }

            // Seed persons
            if (!context.People.Any())
            {
                var citiesList = context.Cities.ToList();
                var rand = new Random();
                var persons = new List<Person>
                {
                    new Person { FirstNameGe="გიორგი", FirstNameEn="Giorgi", LastNameGe="ბერიძე", LastNameEn="Beridze", Gender="Male", PersonalNumber="10000000001", BirthDate=DateTime.Today.AddYears(-25), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ნინო", FirstNameEn="Nino", LastNameGe="შამუგია", LastNameEn="Shamugia", Gender="Female", PersonalNumber="10000000002", BirthDate=DateTime.Today.AddYears(-26), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="მარიამ", FirstNameEn="Mariam", LastNameGe="წიკლაური", LastNameEn="Tsiklauri", Gender="Female", PersonalNumber="10000000003", BirthDate=DateTime.Today.AddYears(-24), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="დავით", FirstNameEn="David", LastNameGe="ჭაბაშვილი", LastNameEn="Chabashvili", Gender="Male", PersonalNumber="10000000004", BirthDate=DateTime.Today.AddYears(-27), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ანა", FirstNameEn="Ana", LastNameGe="გურგენიძე", LastNameEn="Gurgendze", Gender="Female", PersonalNumber="10000000005", BirthDate=DateTime.Today.AddYears(-23), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ლუკა", FirstNameEn="Luka", LastNameGe="გიორგაძე", LastNameEn="Giorgadze", Gender="Male", PersonalNumber="10000000006", BirthDate=DateTime.Today.AddYears(-28), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="თამარ", FirstNameEn="Tamar", LastNameGe="კაპანაძე", LastNameEn="Kapanadze", Gender="Female", PersonalNumber="10000000007", BirthDate=DateTime.Today.AddYears(-22), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ლევან", FirstNameEn="Levan", LastNameGe="ლომიძე", LastNameEn="Lomidze", Gender="Male", PersonalNumber="10000000008", BirthDate=DateTime.Today.AddYears(-29), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ქეთევან", FirstNameEn="Ketevan", LastNameGe="ჯანიკაშვილი", LastNameEn="Janikashvili", Gender="Female", PersonalNumber="10000000009", BirthDate=DateTime.Today.AddYears(-30), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ირაკლი", FirstNameEn="Irakli", LastNameGe="ნიკოლეიშვილი", LastNameEn="Nikoleishvili", Gender="Male", PersonalNumber="10000000010", BirthDate=DateTime.Today.AddYears(-26), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="სალომე", FirstNameEn="Salome", LastNameGe="აბაშიძე", LastNameEn="Abashidze", Gender="Female", PersonalNumber="10000000011", BirthDate=DateTime.Today.AddYears(-24), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="მიხეილ", FirstNameEn="Mikheil", LastNameGe="აბდელაძე", LastNameEn="Abdeladze", Gender="Male", PersonalNumber="10000000012", BirthDate=DateTime.Today.AddYears(-25), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="მაია", FirstNameEn="Maia", LastNameGe="გალობლიშვილი", LastNameEn="Ggaloblishvili", Gender="Female", PersonalNumber="10000000013", BirthDate=DateTime.Today.AddYears(-23), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="გელა", FirstNameEn="Gela", LastNameGe="პაპაშვილი", LastNameEn="Papashvili", Gender="Male", PersonalNumber="10000000014", BirthDate=DateTime.Today.AddYears(-28), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ნანა", FirstNameEn="Nana", LastNameGe="ყიფიანი", LastNameEn="Kipiani", Gender="Female", PersonalNumber="10000000015", BirthDate=DateTime.Today.AddYears(-27), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="დავით", FirstNameEn="David", LastNameGe="ახვლედიანი", LastNameEn="Akhveladze", Gender="Male", PersonalNumber="10000000016", BirthDate=DateTime.Today.AddYears(-29), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="იუნია", FirstNameEn="Yulia", LastNameGe="ალექსიძე", LastNameEn="Alekseishvili", Gender="Female", PersonalNumber="10000000017", BirthDate=DateTime.Today.AddYears(-26), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ზუკა", FirstNameEn="Zuka", LastNameGe="გოლიაძე", LastNameEn="Goliadze", Gender="Male", PersonalNumber="10000000018", BirthDate=DateTime.Today.AddYears(-24), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="ეკატერინე", FirstNameEn="Ekaterine", LastNameGe="შენგელია", LastNameEn="Shengelia", Gender="Female", PersonalNumber="10000000019", BirthDate=DateTime.Today.AddYears(-23), CityId=citiesList[rand.Next(citiesList.Count)].Id },
                    new Person { FirstNameGe="პაატა", FirstNameEn="Paata", LastNameGe="ბარამიძე", LastNameEn="Baramidze", Gender="Male", PersonalNumber="10000000020", BirthDate=DateTime.Today.AddYears(-28), CityId=citiesList[rand.Next(citiesList.Count)].Id }
                };

                foreach (var person in persons)
                {
                    person.PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber { Type = "Mobile", Number = $"599{rand.Next(1000000, 9999999)}" },
                        new PhoneNumber { Type = "Home",   Number = $"322{rand.Next(1000000, 9999999)}" }
                    };
                }

                context.People.AddRange(persons);
                await context.SaveChangesAsync();
            }

            // Seed relations
            if (!context.PersonRelations.Any())
            {
                var allPersons = context.People.ToList();
                var rand = new Random();
                var relationTypes = new[] { "Colleague", "Acquaintance", "Relative", "Other" };
                var relations = new List<PersonRelation>();

                foreach (var person in allPersons)
                {
                    // assign 1-3 random relations per person
                    var count = rand.Next(1, 4);
                    var others = allPersons.Where(p => p.Id != person.Id).OrderBy(_ => rand.Next()).Take(count);
                    foreach (var other in others)
                    {
                        relations.Add(new PersonRelation
                        {
                            PersonId = person.Id,
                            RelatedPersonId = other.Id,
                            RelationType = relationTypes[rand.Next(relationTypes.Length)]
                        });
                    }
                }

                context.PersonRelations.AddRange(relations);
                await context.SaveChangesAsync();
            }
        }
    }
}
