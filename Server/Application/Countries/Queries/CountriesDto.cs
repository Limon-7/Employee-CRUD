using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Countries.Queries
{

	public class KeyValuePairResource
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class CountriesDto: KeyValuePairResource, IMapFrom<Country>
    {
		
		public ICollection<KeyValuePairResource> Cities { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Country, CountriesDto>();
			profile.CreateMap<Country, KeyValuePairResource>();
			profile.CreateMap<City, KeyValuePairResource>();
		}
	}
}
