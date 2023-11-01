using BeautyBooking.Data.Enums;
using BeautyBooking.Models;

namespace BeautyBooking.Data
{
	public class AppDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				context.Database.EnsureCreated();

				//Clients
				if (!context.Clients.Any())
				{
					context.Clients.AddRange(new List<Client>()
					{
						new Client()
						{
							ProfilePhotoURL = "",
							Surname = "Melnichenko",
							Name = "Diana",
							BirthDate = new DateTime(2002, 12, 18),
							Gender = Gender.Female,
							PhoneNumber = "+380991786523",
							Email = "di.melnichenko@gmail.com",
							Password = "Diana_14"
						},
						new Client()
						{
							ProfilePhotoURL = "",
							Surname = "Zinchenko",
							Name = "Olena",
							BirthDate = new DateTime(2003, 12, 10),
							Gender = Gender.Female,
							PhoneNumber = "+38099642423",
							Email = "olenka66@gmail.com",
							Password = "p@ssw0rD."
						},
						new Client()
						{
							ProfilePhotoURL = "",
							Surname = "Bila",
							Name = "Anna",
							BirthDate = new DateTime(1992, 2, 10),
							Gender = Gender.Female,
							PhoneNumber = "+380661554213",
							Email = "bilanna@gmail.com",
							Password = "123Password&^"
						},
						new Client()
						{
							ProfilePhotoURL = "",
							Surname = "Sokolov",
							Name = "Volodimir",
							BirthDate = new DateTime(1990, 4, 15),
							Gender = Gender.Male,
							PhoneNumber = "+380509088224",
							Email = "sokol@gmail.com",
							Password = "Sokol_666"
						},
						new Client()
						{
							ProfilePhotoURL = "",
							Surname = "Smirnov",
							Name = "Mykhail",
							BirthDate = new DateTime(2004, 1, 11),
							Gender = Gender.Other,
							PhoneNumber = "+380980776511",
							Email = "smirnov@gmail.com",
							Password = "Smirn0v__"
						}
					});
					context.SaveChanges();
				}
				//Masters
				if (!context.Masters.Any())
				{
					context.Masters.AddRange(new List<Master>()
					{
						new Master()
						{
							ProfilePhotoURL = "",
							Surname = "Mukhina",
							Name = "Maria",
							Info = "Top master.",
							Email = "mukhina.maria@gmail.com",
							Password = "Mukhin@@M"
						},
						new Master()
						{
							ProfilePhotoURL = "",
							Surname = "Usupova",
							Name = "Olena",
							Info = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident.",
							Email = "uolenka7@gmail.com",
							Password = "uOlenka7++"
						},
						new Master()
						{
							ProfilePhotoURL = "",
							Surname = "Chorna",
							Name = "Anastasia",
							Info = "Best master, works for 3 years.",
							Email = "stacy333@gmail.com",
							Password = "St@sy300"
						},
						new Master()
						{
							ProfilePhotoURL = "",
							Surname = "Zhuk",
							Name = "Sofia",
							Info = "",
							Email = "zhuksofia@gmail.com",
							Password = "ZHukS0fi@"
						}
					});
					context.SaveChanges();
				}
				//Services
				if (!context.Services.Any())
				{
					context.Services.AddRange(new List<Service>()
					{
						new Service()
						{
							Name = "Зняття",
							Description = "Апаратне зняття старого покриття.",
							Duration = 20,
							Price = 100,
						},
						new Service()
						{
							Name = "Покриття",
							Description = "Покриття гель лаком.",
							Duration = 45,
							Price = 300,
						},
						new Service()
						{
							Name = "Манікюр класичний",
							Description = "Тільки для жінок.",
							Duration = 60,
							Price = 360,
						},
						new Service()
						{
							Name = "Укріплення",
							Description = "Укріплення нігтьової пластини гелем або пудрою.",
							Duration = 60,
							Price = 100,
						},
						new Service()
						{
							Name = "Ремон одного нігтя",
							Description = "description",
							Duration = 10,
							Price = 20,
						}
					});
					context.SaveChanges();
				}
				//Notes
				if (!context.Notes.Any())
				{
					context.Notes.AddRange(new List<Note>()
					{
						new Note()
						{
							Title = "Гель-лак блакитний",
							Price = 120,
							Priority = Priority.Medium,
							MasterId = 1
						},
						new Note()
						{
							Title = "Гель-лак червоний",
							Price = 150,
							Priority = Priority.High,
							MasterId = 3
						}
					});
					context.SaveChanges();
				}
				//FreeTime
				if (!context.FreeTimes.Any())
				{
					context.FreeTimes.AddRange(new List<FreeTime>()
					{
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 10, 5, 9, 0, 0),
							MasterId = 4
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 5, 12, 0, 0),
							MasterId = 1
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 5, 14, 0, 0),
							MasterId = 1
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 5, 12, 0, 0),
							MasterId = 2
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 6, 12, 0, 0),
							MasterId = 2
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 7, 12, 0, 0),
							MasterId = 3
						},
						new FreeTime()
						{
							DateAndTime = new DateTime(2023, 11, 7, 16, 0, 0),
							MasterId = 3
						}
					});
					context.SaveChanges();
				}
				//Records
				if (!context.Records.Any())
				{
					context.Records.AddRange(new List<Record>()
					{
						new Record()
						{
							Feedback = null,
							Status = Status.Confirmed,
							ClientId = 1,
							FreeTimeId = 2
						},
						new Record()
						{
							Feedback = null,
							Status = Status.NotConfirmed,
							ClientId = 2,
							FreeTimeId = 3
						},
						new Record()
						{
							Feedback = null,
							Status = Status.Done,
							ClientId = 3,
							FreeTimeId = 1
						},
						new Record()
						{
							Feedback = null,
							Status = Status.NotConfirmed,
							ClientId = 3,
							FreeTimeId = 4
						}
					});
					context.SaveChanges();
				}
				//GroupsOfServices
				if (!context.GroupsOfServices.Any())
				{
					context.GroupsOfServices.AddRange(new List<GroupOfServices>()
					{
						new GroupOfServices()
						{
							ServiceId = 1,
							RecordId = 1
						},
						new GroupOfServices()
						{
							ServiceId = 2,
							RecordId = 1
						},
						new GroupOfServices()
						{
							ServiceId = 1,
							RecordId = 2
						},
						new GroupOfServices()
						{
							ServiceId = 3,
							RecordId = 2
						},
						new GroupOfServices()
						{
							ServiceId = 4,
							RecordId = 4
						}
					});
					context.SaveChanges();
				}
			}

		}

	}
}
