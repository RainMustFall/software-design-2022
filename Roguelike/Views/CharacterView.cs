using System.Data;
using NStack;
using Roguelike.Core.Abstractions.Items;
using Roguelike.Playables;
using Terminal.Gui;

namespace Roguelike.Views;

/// <summary>
/// Class <c>CharacterView</c> is responsible for displaying the state of
/// the character: health, equipment and inventory
/// </summary>
public class CharacterView : View
{
    private ProgressibleHumanoid character;
    private ProgressBar bar;
    private TableView equipment;
    private Label helmet;
    private Label body;
    private Label weapon;
    private ListView inventory;

    public CharacterView(ProgressibleHumanoid character)
    {
        this.character = character;
        Setup();
    }

    private void Setup()
    {
        var frame = new FrameView("Your state")
        {
            X = 0,
            Y = 0,
            Height = Dim.Fill(),
            Width = Dim.Fill()
        };

        Add(frame);
        AddHealthBar(frame);
        AddEquipmentView(frame);
        AddInventoryView(frame);
    }

    void AddInventoryView(FrameView frame)
    {
        var inventoryFrame = new FrameView("Inventory")
        {
            X = 1,
            Y = Pos.Bottom(weapon) + 1,
            Width = Dim.Fill() - 1,
            Height = Dim.Fill()
        };

        inventory = new ListView()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        frame.Add(inventoryFrame);
        inventoryFrame.Add(inventory);
    }

    void AddHealthBar(FrameView frame)
    {
        bar = new ProgressBar()
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Fraction = 0.0f
        };

        frame.Add(bar);
    }

    void AddEquipmentPart(FrameView frame, string title, ref Label labelToInit, View anchor, int offset = 0)
    {
        var boldCaption = new Label(title)
        {
            X = 1,
            Y = Pos.Bottom(anchor) + offset,
            Width = Dim.Fill() - 1,
            ColorScheme = Colors.Dialog
        };

        labelToInit = new Label()
        {
            X = 1,
            Y = Pos.Bottom(boldCaption),
            Width = Dim.Fill(),
        };
        
        frame.Add(boldCaption, labelToInit);
    }
    
    void AddEquipmentView(FrameView frame)
    {
        AddEquipmentPart(frame, "Helmet", ref helmet, bar, offset:1);
        AddEquipmentPart(frame, "Body", ref body, helmet);
        AddEquipmentPart(frame, "Weapon", ref weapon, body);
    }

    void UpdateEquipmentCaption(Label label, IItem? item)
    {
        label.Text = item != null ? item.Name : "(none)";
    }
    
    public override void Redraw(Rect rect)
    {
        bar.Fraction = 1.0f * character.State.CurrentHealth / character.Properties.MaxHealth;
        
        UpdateEquipmentCaption(body, character.Equipment.Body);
        UpdateEquipmentCaption(helmet, character.Equipment.Helmet);
        UpdateEquipmentCaption(weapon, character.Equipment.Weapon);

        inventory.SetSource(character.Inventory.Select(x => x.Name).ToList());

        base.Redraw(rect);
    }
}