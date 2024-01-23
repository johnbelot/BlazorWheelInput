# BlazorWheelPicker
![NuGet Version](https://img.shields.io/nuget/v/BlazorWheelPicker)
![License](https://img.shields.io/badge/license-MIT-green)


BlazorWheelPicker is a tiny NuGet package containing two component for Blazor mimicking the iOS style wheel select input. It is pretty handy for developers using Blazor on MAUI app or for a mobile version of your website.

I used this codepen from [Max Kohler](https://codepen.io/maxakohler) for the css part  which made me discover css scroll snapping : [https://codepen.io/maxakohler/pen/JZgXxe](https://codepen.io/maxakohler/pen/JZgXxe)


## Get Started

Add the NuGet package via **Package Manager**:

```
NuGet\Install-Package BlazorWheelPicker 
```

via **CLI** : 

```
dotnet add package BlazorWheelPicker
```

or simply add the reference in your .csproj file:

```
<PackageReference Include="BlazorWheelPicker" Version="1.0.0" />
```

Add the **css** reference to your **index.html** file located in /wwwroot

```html
<link href="_content/BlazorWheelPicker/BlazorWheelPicker.css" rel="stylesheet" />
```

And the **javascript**:

```html
<script src="_content/BlazorWheelPicker/wheelpicker.js"></script>
```

And finally add the namespace reference to **_Imports.razor** for more practicality:

```razor
@using BlazorWheelPickerLib;
```

## Usage

This contains 2 components : **WheelSelect** and **DateWheelSelect**, the later is just an implementation for a date picker

### WheelSelect

|Name| Type | Description  | Default|
|---|---|---|---|
| Style |  string |  Additional style you want to apply | - |
| T |   |  Type of the selectable items | - |
| Items  |  T | Selectable item list  | - |
| WheelLevel  | int  |  Level of displayed items | 2 |
| SelectionLines  | boolean  |  Whether or not selection line in the middle are displayed| true |
| Dense  | boolean  |  Whether or not the item rows are dense| false |
| WheelColor  | string  |  Hexadecimal value of the background wheel color| #ffffff |
| TextColor  | string  |  Hexadecimal value of the text color| #000000 |
| Value  | T  |  Selected value of the picker (or use @bind-Value)| -|
| ValueChanged  |  EventCallback\<T>  |  Value changed event (or use @bind-Value)| - |
| ItemTemplate  |  RenderFragment\<T>  |  Custom render template for rows| - |

#### Example:
```razor
<WheelSelect @bind-Value="_selectedCity" Style="width:50%" Dense="true" T="string" Items="Items">
    <ItemTemplate Context="test">
        <div style="display:flex;flex-direction:row;align-items:center;justify-content:center">
            @test
        </div>
    </ItemTemplate>
</WheelSelect>
@code
{
    public List<string> Items = new List<string>() { "Paris", "Rome", "London", "Berlin", "Madrid" };
    private string _selectedCity = "Paris";
}
```

![Example of wheel select](https://github.com/johnbelot/BlazorWheelInput/blob/main/SimpleWheel.gif)

### DateWheelSelect

|Name| Type | Description  | Default|
|---|---|---|---|
| Style |  string |  Additional style you want to apply | - |
| WheelLevel  | int  |  Level of displayed items | 2 |
| Dense  | boolean  |  Whether or not the item rows are dense| false |
| SelectionLines  | boolean  |  Whether or not selection line in the middle are displayed| true |
| WheelColor  | string  |  Hexadecimal value of the background wheel color| #ffffff |
| TextColor  | string  |  Hexadecimal value of the text color| #000000 |
| Value  | DateTime  |  Selected DateTime value of the picker (or use @bind-Value)| -|
| ValueChanged  |  EventCallback \<DateTime>  |  DateTime Value changed event (or use @bind-Value)| - |
| MinYear  |  DateTime?  |  Minimum year displayed| 1970 |
| MaxYear  |  DateTime?  |  Maximum year displayed| 2070 |

#### Example:
```razor
<DateWheelSelect MaxYear="DateTime.Now.AddYears(40)" MinYear="DateTime.Now.AddYears(-40)" WheelLevel="2" Dense="false" @bind-Value="SelectedDate">
</DateWheelSelect>
<p>Selected Date : @SelectedDate.ToString("dd-MM-yyyy")</p>
@code
{
    DateTime SelectedDate = DateTime.Now;
}
```

![Example of date wheel select](https://github.com/johnbelot/BlazorWheelInput/blob/main/DateWheel.gif)


## Misc

If used with MAUI, i suggest to add a vibration when a value is changed for a native feel:

```cs
HapticFeedback.Default.Perform(HapticFeedbackType.Click);
```

## License

This package is under the MIT license, feel free to fork it and modify it.
