﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Application.Documents.Abstractions;
using AutoMapper;

namespace Application.Documents.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type> types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IMappable)))
                .ToList();

            foreach (Type type in types)
            {
                object? instance = Activator.CreateInstance(type);

                MethodInfo? methodInfo = type.GetMethod(nameof(IMappable.Mapping));

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
