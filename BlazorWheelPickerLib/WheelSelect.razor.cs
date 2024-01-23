using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWheelPickerLib
{
    public partial class WheelSelect<T> : ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public List<T> Items { get; set; }

        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public int WheelLevel { get; set; } = 2;

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool SelectionLines { get; set; } = true;

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public string WheelColor { get; set; } = "#ffffff";

        [Parameter]
        public string TextColor { get; set; } = "#000000";

        [Parameter]
        public RenderFragment<T> ItemTemplate { get; set; }

        ElementReference WheelContainer { get; set; }

        private float Height
        {
            get
            {
                return ((WheelLevel * 2) + 1) * _elementHeight;
            }
        }

        private float _elementHeight => Dense ? 1.4f : 2.4f;
        private int _selectedIndex;
        private Guid _instanceId = Guid.NewGuid();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("setupScrollSnapDetection", WheelContainer, DotNetObjectReference.Create(this), WheelLevel);


                if (Value != null)
                {
                    await JSRuntime.InvokeVoidAsync("scrollToItem", WheelContainer, Items.IndexOf(Value), WheelLevel);
                }
            }
        }

        protected override void OnInitialized()
        {
            if (Items == null)
            {
                throw new Exception("[Wheel Picker]: Missing Items property");
            }
        }

        public string GetClass(int index)
        {
            var itemClass = Dense ? "scroll-item dense" : "scroll-item";
            return _selectedIndex == index ? "scroll-item active" : "scroll-item";
        }

        [JSInvokable]
        public void SetSnappedElement(int index)
        {
            if (index == _selectedIndex)
            {
                return;
            }

            _selectedIndex = index;
            ValueChanged.InvokeAsync(Items[index]);
            StateHasChanged();
        }

        private string GetColorWithOpacity(string hexColor, float opacityPercentage)
        {
            // Ensure the opacity is within the range [0, 100]
            opacityPercentage = Math.Max(0, Math.Min(100, opacityPercentage));

            // Convert the opacity percentage to a value between 0 and 255
            int alpha = (int)(255 * (opacityPercentage / 100));

            // Convert hex string to Color
            Color originalColor = ColorTranslator.FromHtml(hexColor);

            // Create new Color with the same RGB values but with new alpha
            Color colorWithOpacity = Color.FromArgb(alpha, originalColor.R, originalColor.G, originalColor.B);

            return $"#{colorWithOpacity.R:X2}{colorWithOpacity.G:X2}{colorWithOpacity.B:X2}{colorWithOpacity.A:X2}";
        }
    }
}
