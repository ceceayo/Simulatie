using Microsoft.VisualBasic;
using Simulatie;

namespace Terbeeldbrenger;

public class GuiMode : IGuiMode
{
    public string InputBox(string prompt, string title, string defaultValue)
    {
        return Interaction.InputBox(prompt, title, defaultValue);
    }
}