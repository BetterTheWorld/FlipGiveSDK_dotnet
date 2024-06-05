using FlipGiveSDK_dotnet;
using FlipGiveSDK_dotnet.Exceptions.PayloadExceptions;
using FlipGiveSDK_dotnet.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Test_FlipGiveSDK_dotnet.Methods
{
    public class SDKIdentifiedTokenTest
    {
        private readonly ServiceProvider _provider;

        public SDKIdentifiedTokenTest()
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.UseFlipGiveRewards(config);

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task TestSuccessWithUserAndCampaignIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            #endregion

            #region Act

            var token = flipGiveRewardsService.IdentifiedToken(payload);

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };

            using HttpClient client = new(handler);
            var response = await client.GetAsync($"https://cloud.flipgive-test.com/?token={token}");

            #endregion

            #region Assert

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            #endregion
        }

        [Fact]
        public async Task TestSuccessWithUserDataIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
            };

            #endregion

            #region Act

            var token = flipGiveRewardsService.IdentifiedToken(payload);

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };

            using HttpClient client = new(handler);
            var response = await client.GetAsync($"https://cloud.flipgive-test.com/?token={token}");

            #endregion

            #region Assert

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            #endregion
        }

        [Fact]
        public async Task TestSuccessWithCampaignDataIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            #endregion

            #region Act

            var token = flipGiveRewardsService.IdentifiedToken(payload);

            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };

            using HttpClient client = new(handler);
            var response = await client.GetAsync($"https://cloud.flipgive-test.com/?token={token}");

            #endregion

            #region Assert

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            #endregion
        }

        [Fact]
        public void TestNullIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(null);
            }
            catch (Exception missingPayloadException)
            {
                ex = missingPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is MissingPayloadException);
            Assert.Equal("Payload is null", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoUserDataOrCampaignDataIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is MinimumPayloadException);
            Assert.Equal("Payload doesn't contain minimal data: user_data or campaign_data", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoUserDataIdIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field id in property user_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoUserDataNameIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field name in property user_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoUserDataEmailIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field email in property user_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoUserDataCountryIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldOutsideOfInclusionException);
            Assert.Equal("Required field country in property user_data is not part of: CAN,USA", ex.Message);

            #endregion
        }

        [Fact]
        public void TestWrongUserDataCountryIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "ROM"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldOutsideOfInclusionException);
            Assert.Equal("Required field country in property user_data is not part of: CAN,USA", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoCampaignDataIdIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field id in property campaign_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoCampaignDataNameIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field name in property campaign_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoCampaignDataCategoryIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field category in property campaign_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoCampaignDataCountryIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldOutsideOfInclusionException);
            Assert.Equal("Required field country in property campaign_data is not part of: CAN,USA", ex.Message);

            #endregion
        }

        [Fact]
        public void TestWrongCampaignDataCountryIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "ROM",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldOutsideOfInclusionException);
            Assert.Equal("Required field country in property campaign_data is not part of: CAN,USA", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoGroupDataNameIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                },
                GroupData = new GroupData()
                {
                    PlayerNumber = "number 12"
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field name in property group_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoOrganizationDataIdIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                },
                OrganizationData = new OrganizationData()
                {
                    Name = "Organization156",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field id in property organization_data is missing", ex.Message);

            #endregion
        }

        [Fact]
        public void TestNoOrganizationDataNameIdentifiedTokenAsync()
        {
            #region Arrange

            var flipGiveRewardsService = _provider.GetRequiredService<FlipGiveRewardsService>();
            var payload = new Payload()
            {
                UserData = new UserData()
                {
                    Id = "1709224272",
                    Name = "Marty 1709224272",
                    Email = "marty_1709224272@timetravel.io",
                    Country = "CAN"
                },
                CampaignData = new CampaignData()
                {
                    Id = "1709224272",
                    Name = "Campaign1709224272",
                    Category = "Running",
                    Country = "CAN",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                },
                OrganizationData = new OrganizationData()
                {
                    Id = "Organization156",
                    AdminData = new UserData()
                    {
                        Id = "1709224272",
                        Name = "Marty 1709224272",
                        Email = "marty_1709224272@timetravel.io",
                        Country = "CAN"
                    }
                }
            };

            Exception? ex = null;

            #endregion

            #region Act

            try
            {
                var token = flipGiveRewardsService.IdentifiedToken(payload);
            }
            catch (Exception minimumPayloadException)
            {
                ex = minimumPayloadException;
            }
            #endregion

            #region Assert

            Assert.NotNull(ex);
            Assert.True(ex is RequiredFieldInPayloadMissingException);
            Assert.Equal("Required field name in property organization_data is missing", ex.Message);

            #endregion
        }
    }
}