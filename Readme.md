# BlazorWheelPicker
--

BlazorWheelPicker is a tiny NuGet package containing two component for Blazor mimicking the iOS style wheel select input. It is pretty handy for developers using Blazor for MAUI app of for a mobile version of your website.

I used this codepen for the css part from [Max Kohler](https://codepen.io/maxakohler) which made me discover css scroll snapping : [https://codepen.io/maxakohler/pen/JZgXxe](https://codepen.io/maxakohler/pen/JZgXxe)


## Get Started

Add the NuGet package via Package Manager:
```NuGet\Install-Package BlazorWheelPicker -Version 1.0.0``

or simply add the reference in your .csproj file:

```<PackageReference Include="BlazorWheelPicker" Version="1.0.0" />```

Add the **css** reference to your **index.html** file located in /wwwroot

```<link href="_content/BlazorWheelPicker/BlazorWheelPicker.css" rel="stylesheet" />```

And the **javascript**:

```<script src="_content/BlazorWheelPicker/wheelpicker.js"></script>```

And finally add the namespace reference to _Imports.razor for more practicality:

```@using BlazorWheelPickerLib;```

## Usage

This contains 2 components : **WheelSelect** and **DateWheelSelect**, the later is just an implementation for a date picker

### WheelSelect

|Name| Type | Description  | Default|
|---|---|---|---|
| Style |  string |  Additional style you want to apply | - |
| T |   |  Type of the selectable items | - |
| Items  |  T | Selectable item list  | - |
| WheelLevel  | int  |  Level of displayed items | 2 |
| Dense  | boolean  |  Whether or not the item rows are dense| false |
| WheelColor  | string  |  Hexadecimal value of the background wheel color| #ffffff |
| TextColor  | string  |  Hexadecimal value of the text color| #000000 |
| Value  | T  |  Selected value of the picker (or use @bind-Value)| -|
| ValueChanged  |  EventCallback<T>  |  Value changed event (or use @bind-Value)| - |
| ItemTemplate  |  RenderFragment<T>  |  Custom render template for rows| - |

#### Example:
```
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

![Example of wheel select]()

### DateWheelSelect

|Name| Type | Description  | Default|
|---|---|---|---|
| Style |  string |  Additional style you want to apply | - |
| WheelLevel  | int  |  Level of displayed items | 2 |
| Dense  | boolean  |  Whether or not the item rows are dense| false |
| WheelColor  | string  |  Hexadecimal value of the background wheel color| #ffffff |
| TextColor  | string  |  Hexadecimal value of the text color| #000000 |
| Value  | DateTime  |  Selected DateTime value of the picker (or use @bind-Value)| -|
| ValueChanged  |  DateTime  |  DateTime Value changed event (or use @bind-Value)| - |

#### Example:
```
<DateWheelSelect WheelLevel="2" Dense="true" @bind-Value="SelectedDate">
</DateWheelSelect>
<p>Selected Date : @SelectedDate.ToString("dd-MM-yyyy")</p>
@code
{
    DateTime SelectedDate = DateTime.Now;
}
```

![Example of date wheel select]()


## Misc

If used with MAUI, i suggest to add a vibration when a value is changed :

```HapticFeedback.Default.Perform(HapticFeedbackType.Click);```

## License

This package is under the MIT license, feel free to fork it and modify it.