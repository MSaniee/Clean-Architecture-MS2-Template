using AutoMapper;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Application.Mapping.Profiles
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> haveCustomMappings)
        {
            foreach (var item in haveCustomMappings)
                item.CreateMappings(this);
        }
    }
}
