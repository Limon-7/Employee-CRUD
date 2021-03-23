using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seed
{
	public class Seed
	{

		public static void SeedData(ApplicationDbContext context)
		{
			if (!context.Countries.Any())
			{
				var countrines = new List<Country>
				{
					new Country
					{
						
						Name="Bangladesh"
					},
					new Country
					{
						Name="India"
					},
					new Country
					{
						Name="Nepal"
					},
					new Country
					{
						Name="Spain"
					},
					new Country
					{
						Name="USA"
					},
				};
				context.Countries.AddRange(countrines);
				context.SaveChanges();
			}
			if (!context.Cities.Any())
			{
				var cities = new List<City>
				{
					new City
					{
						Name="Dhaka",
						CountryId=1
					},
					new City
					{
						Name="Rangpur",
						CountryId=1
					},
					new City
					{
						Name="Rajshahi",
						CountryId=1
					}, 
					new City
					{
						Name="Kalkata",
						CountryId=2
					}, new City
					{
						Name="Barcelona",
						CountryId=4
					}
				};
				context.Cities.AddRange(cities);
				context.SaveChanges();
			}
			if (!context.Skills.Any())
			{
				var skills = new List<Skill>
				{
					new Skill
					{

						LanguageSkill="C+"
					},
					new Skill
					{
						LanguageSkill="JavaScript"
					},
					new Skill
					{
						LanguageSkill="C#"
					},
					new Skill
					{
						LanguageSkill="Angular"
					},
					new Skill
					{
						LanguageSkill="React"
					}
				};
				context.Skills.AddRange(skills);
				context.SaveChanges();
			}
			

		}
	}
}
