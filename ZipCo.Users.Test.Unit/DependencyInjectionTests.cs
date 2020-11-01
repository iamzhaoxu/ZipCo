using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZipCo.Users.Infrastructure.Bootstrap.Extensions;

namespace ZipCo.Users.Test.Unit
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void GivenInjectDependencies_ShouldSuccess()
        {
            // Arrange
            var configuration = SetupConfigurationRoot();
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.AddDependencies();
            serviceCollection.AddDbContext(configuration);

             // Assert
            var exceptions = new List<InvalidOperationException>();
            using (var provider = serviceCollection.BuildServiceProvider())
            {
                foreach (var serviceDescriptor in serviceCollection)
                {
                    var serviceType = serviceDescriptor.ServiceType;
                    if (serviceType.Namespace != null && serviceType.Namespace.StartsWith("ZipCo.Users"))
                    {
                        try
                        {
                            provider.GetService(serviceType);
                        }
                        catch (InvalidOperationException e)
                        {
                            exceptions.Add(e);
                        }
                    }
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException("Some services are missing", exceptions);
            }
        }

        private static ConfigurationRoot SetupConfigurationRoot()
        {
            var configuration = new ConfigurationRoot(new List<IConfigurationProvider>
            {
                new MemoryConfigurationProvider(new MemoryConfigurationSource
                {
                    InitialData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("ConnectionStrings:UserDbConnection",
                            "Server=localhost;Database=PayCo.User;user id=sa;password=123;")
                    }
                })
            });
            return configuration;
        }
    }
}
