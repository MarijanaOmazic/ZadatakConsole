using System.Data;
using ZadatakConsole.Data;
using ZadatakConsole.Models;

namespace ZadatakConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer;
            do
            {
                Console.WriteLine("Odaberi opciju\n" +
                "1.Kreiraj novog korisnika\n" +
                "2.Ispiši korisnike po nazivu abecedno\n" +
                "3.Ažuriraj odabranog korisnika\n" +
                "4.Izbriši odabranog korisnika");
                int choice = int.Parse(Console.ReadLine());
                int idUser;

                switch (choice)
                {
                    case 1:
                        do
                        {
                            CreateUser();
                            Console.Write("Želite li nastaviti s unosom korisnika (Da/Ne): ");
                            answer = Console.ReadLine();
                        } while (answer != "Ne" && answer != "NE" && answer != "ne");
                        ReadUser();
                        break;
                    case 2:
                        ReadUserOrderBy();
                        break;
                    case 3:
                        Console.Write("Unesite Id korsnika kojeg želite ažurirati:");
                        idUser = Convert.ToInt32(Console.ReadLine());
                        UpdateUser(idUser);
                        ReadUser();
                        break;
                    case 4:
                        Console.Write("Unesite Id korsnika kojeg želite izbrisati:");
                        idUser = Convert.ToInt32(Console.ReadLine());
                        DeleteUser(idUser);
                        ReadUser();
                        break;
                    default:
                        Console.WriteLine("Molimo Vas odaberite validan broj:");
                        break;
                }
                Console.Write("Želite li nastaviti dalje (Da/Ne)? ");
                answer = Console.ReadLine();

            } while (answer != "Ne" && answer != "NE" && answer != "ne");

        }
        static void CreateUser()
        {
            using (var db = new AppDbContext())
            {
                User user = new User();
                Console.Write("Unesite ime korisnika:");
                user.Name = Console.ReadLine();
                Console.Write("Unesite email korisnika:");
                user.Email = Console.ReadLine();
                bool hasAtSymbol = user.Email.Contains("@");

                if (hasAtSymbol)
                {
                    db.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Email ne sadrži @ simbol!");
                }
            }
            return;
        }
        static void ReadUserOrderBy()
        {
            using (var db = new AppDbContext())
            {
                List<User> user = db.Users.OrderBy(user => user.Name).ToList();
                Console.WriteLine("---Popis svih korisnika abecedno po imenu!---");
                Console.WriteLine("NAME \tEMAIL");
                foreach (User u in user)
                {
                    Console.WriteLine("{0} \t{1}", u.Name, u.Email);
                }

            }
            return;
        }
        static void ReadUser()
        {
            using (var db = new AppDbContext())
            {
                List<User> user = db.Users.ToList();
                Console.WriteLine("---Popis svih korisnika!---");
                Console.WriteLine("ID \tNAME \tEMAIL");
                foreach (User u in user)
                {
                    Console.WriteLine("{0} \t{1} \t{2}", u.Id, u.Name, u.Email);
                }
            }
            return;
        }
        static void UpdateUser(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    Console.WriteLine("Odabrani korisnik je: Ime - {0}, Email - {1}.", user.Name, user.Email);
                    Console.Write("Novo ime korisnika:");
                    user.Name = Console.ReadLine();
                    Console.Write("Novi email korisnika:");
                    user.Email = Console.ReadLine();
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Korisnik s Id={0} ne postoji!", userId);
                }
            }
            return;
        }
        static void DeleteUser(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    Console.WriteLine("Korisnik kojeg brišemo je {0}, {1}, {2}", user.Id, user.Name, user.Email);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    Console.WriteLine("Uspješno izbrisan korisnik!");
                }
                else
                {
                    Console.WriteLine("Korisnik s Id={0} ne postoji!", userId);
                }
            }
        }
    }
}