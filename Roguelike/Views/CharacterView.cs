using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using NStack;
using Roguelike.Controllers;
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
    private readonly InventoryEquipmentController inventoryEquipmentController;
    private readonly ProgressibleHumanoid character;
    private ProgressBar health;
    private ProgressBar experience;
    private ProgressBar level;
    private TableView equipment;
    private Label helmet;
    private Label body;
    private Label weapon;
    private ListView inventory;
    
    public CharacterView(InventoryEquipmentController inventoryEquipmentController, ProgressibleHumanoid character)
    {
        this.inventoryEquipmentController = inventoryEquipmentController;
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
        
        health = new ProgressBar();
        experience = new ProgressBar();
        level = new ProgressBar();

        AddCaptionAndBar(0,0, "health", frame, health);
        AddCaptionAndBar(0,2, "exp:", frame, experience);
        AddCaptionAndBar(0,4, "level", frame, level);
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

    static void AddCaptionAndBar(int x, int y, string title, FrameView frame, ProgressBar progressBar)
    {
        var caption = new Label(title)
        {
            X = x,
            Y = y,
            Width = 6,
            ColorScheme = Colors.Base
        };
        progressBar.X = Pos.Right(caption);
        progressBar.Y = y;
        progressBar.Width = Dim.Fill();
        progressBar.Fraction = 0.0f;
        frame.Add(caption, progressBar);
    }
    
    void AddEquipmentPart(FrameView frame, string title, ref Label labelToInit, View anchor, Key shortcut, Action action, int offset = 0)
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
            Shortcut = shortcut,
            ShortcutAction = action
        };
        
        frame.Add(boldCaption, labelToInit);
    }
    
    void AddEquipmentView(FrameView frame)
    {
        AddEquipmentPart(frame, "Helmet", ref helmet, level, Key.CtrlMask | Key.ShiftMask | Key.D1, UnwearHelmetShortcutAction, offset:1);
        AddEquipmentPart(frame, "Body", ref body, helmet, Key.CtrlMask | Key.ShiftMask | Key.D2, UnwearBodyShortcutAction);
        AddEquipmentPart(frame, "Weapon", ref weapon, body, Key.CtrlMask | Key.ShiftMask | Key.D3, UnwearWeaponShortcutAction);
    }
    
    void UpdateEquipmentCaption(Label label, IItem? item)
    {
        label.Text = item != null ? item.Name : "(none)";
    }
    
    public override void Redraw(Rect rect)
    {
        health.Fraction = 1.0f * character.State.CurrentHealth / character.Properties.MaxHealth;
        experience.Fraction = 1.0f * character.Progression.Experience / character.Progression.MaxExperience;
        level.Fraction = 1.0f * character.Progression.Level / character.Progression.MaxLevel;
        
        UpdateEquipmentCaption(body, character.Equipment.Body);
        UpdateEquipmentCaption(helmet, character.Equipment.Helmet);
        UpdateEquipmentCaption(weapon, character.Equipment.Weapon);

        inventory.SetSource(character.Inventory.Select(x => x.Name).ToList());

        base.Redraw(rect);
    }
    
    /// <summary>
    /// Process shortcuts
    /// Ctrl+N: put inventory number N
    /// </summary>
    /// <param name="kb"></param>
    /// <returns></returns>
    public override bool ProcessHotKey (KeyEvent kb)
    {
        // put inventory:
        if (kb.IsCtrl && !kb.IsShift)
            return TryPutInventoryUseShortcut(kb.Key);
        return false;
    }

    private static bool TryParseKeyString(string? numberString, out int number)
    {
        if (numberString!.FirstOrDefault() != 'D')
        {
            number = -1;
            return false;
        }
        var numberStringShort = numberString?.Substring(1);
        return int.TryParse(numberStringShort, out number);
    }
    
    private bool TryPutInventoryUseShortcut(Key key)
    {
        var keysString = key.ToString().Split(',').FirstOrDefault();
        if (TryParseKeyString(keysString, out int number))
        {
            var item = character.Inventory.GetItemByIndex(number - 1);
            inventoryEquipmentController.PutItemOn(character, item);
        }
        return false;
    }
    
    private void UnwearHelmetShortcutAction()
    {
        inventoryEquipmentController.UnwearHelmet(character);
    }
    
    private void UnwearBodyShortcutAction()
    {
        inventoryEquipmentController.UnwearBody(character);
    }
    
    private void UnwearWeaponShortcutAction()
    {
        inventoryEquipmentController.UnwearWeapon(character);
    }
}