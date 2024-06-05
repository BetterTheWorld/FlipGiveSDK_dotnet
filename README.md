# FlipgiveSDK

## Rewards

Rewards is [FlipGive's](https://www.flipgive.com) drop-in cashback program. If you would like to know more please contact us at partners@flipgive.com.

### Links of Interest

- [FlipGive](https://www.flipgive.com)
- [API Documentation](https://docs.flipgive.com)

### Installation

To begin using `FlipGiveRewardsService`, you should have obtained an `ID` and `Secret` pair from FlipGive, store these securely so that they are accessible in your application (env variables, rails credentials, etc). We'll be using env variables in our example below. If you haven't received credentials, please contact us at partners@flipgive.com.

NuGet:
https://www.nuget.org/packages/FlipGiveSDK-dotnet/

```
Install-Package FlipGiveSDK-dotnet
```

Manual:
Grab source and compile yourself:
```
1. dotnet restore
```
```
2. dotnet pack -c Release
```

After you have installed the package include the code below to appsettings.json so the sdk will be initialized:

```
{
  "FlipGiveRewardsOptions": {
    "CloudShopId": "[Id]",
    "Secret": "[Secret]"
  }
}
```

The SDK is now ready to use.

### Usage

#### Injection

In order to use the service it needs to be injected in the IServiceCollection, we provide following ways to initialize:

##### 1. Using IConfiguration
It needs to have our options declared in appsettings.json
```C#
_services.UseFlipGiveRewards(_config);
```

##### 2.a Using Actions
It needs to have our options declared in appsettings.json
```C#
_services.UseFlipGiveRewards(options => _config.GetSection("FlipGiveOptions").Bind(options));
```

##### 2.b Using Actions
It needs to have options declared on the go
```C#
_services.UseFlipGiveRewards(options =>
            {
                options.CloudShopId = "[Id]";
                options.Secret = "[Secret]";
            });
```

##### 3 Using Parameters
It needs to have options declared on the go
```C#
_services.UseFlipGiveRewards("[Id]", "[Secret]");
```

#### Consumption

You can consume the service in any of your classes, just add it to constructor
```C#
private readonly FlipGiveRewardsService _flipGiveRewardsService;

//constructor
public MyClass(FlipGiveRewardsService flipGiveRewardsService) {
    _flipGiveRewardsService = flipGiveRewardsService;
}
```

#### Methods

The main purpose of `FlipGiveRewardsService` is to generate Tokens to gain access to FlipGive's Rewards API. There are 4 methods available on the service.

##### :ReadToken
This method is used to decode a token that has been generated with your credentials. It takes a single string as an argument and, if able to decode the token, it will return a Payload oject.

```C#
token = "eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIn0..demoToken.g8PZPWb1KDFcAkTsufZq0w@A2DE537C"

_flipGiveRewardsService.ReadToken(token)
=> { user_data: { id: 1, name: 'Emmett Brown', email: 'ebrown@time.ca', country: 'USA' } }
```

##### :IdentifiedToken
This method is used to generate a token that will identify a user or campaign. It accepts a **Payload** as an argument and it returns an encrypted token. 

```C#
var payload = new Payload()
            {
                UserData = userData,
                CampaignData = campaignData,
                GroupData = groupData,
                OrganizationData = organizationData,
                UtmData = utmData,
                Type = type,
                Expires = expires
            };

_flipGiveRewardsService.IdentifiedToken(payload);
=> "eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIn0..demoToken.g8PZPWb1KDFcAkTsufZq0w@A2DE537C"
```

The variable in this example uses other variables, (UserData, CampaignData, etc.).
They will be deserialized in Snake case ex:"user_data"
Let's look at each one of them:

- `UserData`: **required** when `CampaignData` is not present in the payload, otherwise optional. It represents the user using the Shop, and  contains the following information:
  - `Id`: **required**. A string representing the user's ID in your system.
  - `Email`: **required**. A string with the user's email.
  - `Name`: **required**. A string with the user's name.
  - `Country`: **required**. A string with the ISO code of the user's country, which must be 'CAN' or 'USA' at this time.
  - `City`: *optional*. A string with the user's city.
  - `State`: *optional*. A string with the user's state. It must be a 2 letter code. You can see a list of values [here](https://github.com/BetterTheWorld/FlipGiveSDK_Ruby/blob/main/states.yml).
  - `PostalCode`: A string with the user's postal code. It must match Regex `/\d{5}/` for the USA or `/[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d/` for Canada.
  - `Latitude`: *optional*. A float with the user's latitude in decimal degree format. Without accompanying `:longitude`, latitude will be ignored.
  - `Longitude`: *optional*. A float with the user's longitude in decimal degree format. Without accompanying `:latitude`, longitude will be ignored.
  - `ImageUrl`: *optional*. A string containing the URL for the user's avatar.

  ```C#
  var userData = new UserData()
        {
            Id = "19850703",
            Name = "Emmett Brown",
            Email = "ebrown@time.com",
            Country = "USA"
        }
  ```
Optional fields of invalid formats will not be validated but will be ignored.

- `CampaignData`: Required when user_data is not present in the payload, otherwise optional. It represents the fundraising campaign and contains the following information:

  - `Id`: **required** A string representing the user's ID in your system.
  - `Name`: **required** A string  with the campaign's email.
  - `Category`: **required** A string  with the campaign's category. We will try to match it with one of our existing categories, or assign a default. You can see a list of our categories [here](https://github.com/BetterTheWorld/FlipGiveSDK_Ruby/blob/main/categories.txt).
  - `Country`: **required** A string  with the ISO code of the campaign's country, which must be 'CAD' or 'USA' at this time.
  - `AdminData`: **required** The user information for the campaign's admin. It must contain the same information as `user_data`
  - `City`: *optional*. A string with the campaign's city.
  - `State`: *optional*. A string with the campaign's state. It must be a 2 letter code. You can see a list [here](https://github.com/BetterTheWorld/FlipGiveSDK_Ruby/blob/main/states.yml).
  - `PostalCode`: A string with the campaign's postal code. It must match Regex `/\d{5}/` for the USA or `/[a-zA-Z]\d[a-zA-Z]\d[a-zA-Z]\d/` for Canada.
  - `Latitude`: *optional*. A float with the campaign's latitude in decimal degree format.
  - `Longitude`: *optional*. A float with the campaign's longitude in decimal degree format.
  - `ImageUrl`: *optional*. A string containing the URL for the campaign's image, if any.

Optional fields of invalid formats will not be validated but will be ignored.

  ```C#
  var campaignData = new CampaignData()
        {
            Id = "19551105",
            Name = "The Time Travelers",
            Category = "Events & Trips",
            Country = "USA",
            AdminData = userData
        }
  ```

- `GroupData`: *Always optional*. Groups are aggregators for users within a campaign. For example, a group can be a Player on a sport's team and the users would be the people supporting them.
  - `Name`: **required**. A string with the group's name.
  - `PlayerNumber`: *optional*. A sport's player number on the team.

  ```C#
  var groupData = new GroupData()
        { 
            Name = 'Marty McFly' 
        }
  ```

- `OrganizationData`: Always optional. Organizations are used to group campaigns. As an example: A School (organization) has many Grades (campaigns), with Students (groups) and Parents (users) shopping to support their student.
  - `Id`: **required**. A string with the organization's ID.
  - `Name`: **required**. A string with the organization's name.
  - `OrganizationAdmin`: **required**. The user information for the organization's admin. It must contain the same information as `user_data`

  ```C#
  var organizationData = new OrganizationData()
        {
            Id = "980",
            Name = 'Back to the Future',
            AdminData = userData
        }
  ```

- `UtmData`:  Always optional. UTM data will be saved when a campaign and/or user is created.
  - `UtmMedium`: A string representing utm_medium.
  - `UtmCampaign`: A string representing utm_campaign.
  - `UtmTerm`: A string representing utm_term.
  - `UtmContent`: A string representing utm_content.
  - `UtmChannel`: A string representing utm_channel.

  ```C#
  var utmData = new UtmData()
        {
            UtmMedium = "Universal Pictures",
            UtmCampaign = "Movie",
            UtmTerm = "Time, Travel",
            UtmContent = "Image",
            UtmChannel = "Time"
          }
  ```

#### :IsValidIdentified
This method is used to validate a payload, without attempting to generate a token. It returns a Boolean. The same rules for `:IdentifiedToken` apply here as well.

```C#
var payload = new Payload
{ 
  UserData = userData 
}

_flipGiveRewardsService.IsValidIdentified(payload);
=> true
```

#### :GetPartnerToken
This method is used to generate a token that can **only** be used by the Rewards partner (that's you) to access reports and other API endpoints. It is only valid for an hour. 

```C#
_flipGiveRewardsService.GetPartnerToken()
=> "eyJhbGciOiJkaXIiLCJlbmMiOiJBMTI4R0NNIn0..demoToken.h9QXQEn2LFGVSlTdiGXW1e@A2DE537C"
```

#### :Exceptions
Validation Exceptions that occur while attempting to generate a token can be retrieved here.

```C#
InvalidTokenException
MissingPayloadException
MinimumPayloadException
RequiredFieldInPayloadMissingException
RequiredFieldOutsideOfInclusionException
```

```C#
userData.Country = 'ENG'

var payload = new Payload()
{ 
  UserData = userData 
}

try
{
    _flipGiveRewardsService.IsValidIdentified(payload);
}
catch (RequiredFieldOutsideOfInclusionException ex)
{
    Console.WriteLine(ex.Message)
=>Required field country in property user_data is not part of: CAN,USA
}
```

### Support

For developer support please open an [issue](https://github.com/BetterTheWorld/FlipGiveSDK_dotnet/issues) on this repository.

### Contributing

Bug reports and pull requests are welcome on GitHub at [https://github.com/BetterTheWorld/FlipGiveSDK_dotnet](https://github.com/BetterTheWorld/FlipGiveSDK_dotnet).

## License

This library is distributed under the
[Apache License, version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html)

```no-highlight
copyright 2023. FlipGive, inc. all rights reserved.

licensed under the apache license, version 2.0 (the "license");
you may not use this file except in compliance with the license.
you may obtain a copy of the license at

    http://www.apache.org/licenses/license-2.0

unless required by applicable law or agreed to in writing, software
distributed under the license is distributed on an "as is" basis,
without warranties or conditions of any kind, either express or implied.
see the license for the specific language governing permissions and
limitations under the license.
```