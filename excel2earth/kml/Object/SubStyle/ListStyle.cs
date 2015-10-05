// CLASSIFICATION: UNCLASSIFIED

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace excel2earth.kml
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class ListStyle : SubStyle
    {
        public enum listItemTypeEnum
        {
            check,             // (default) - The Feature's visibility is tied to its item's checkbox.
            checkOffOnly,      // When specified for a Container, only one of the Container's items is visible at a time.
            checkHideChildren, // When specified for a Container or Network Link, prevents all items from being made visible at once—that is, the user can turn everything in the Container or Network Link off but cannot turn everything on at the same time. This setting is useful for Containers or Network Links containing large amounts of data.
            radioFolder        // Use a normal checkbox for visibility but do not display the Container or Network Link's children in the list view. A checkbox allows the user to toggle visibility of the child objects in the viewer.
        }

        private listItemTypeEnum listItemType;
        private Color bgColor;

        public struct ItemIcon
        {
            public enum itemIconModeEnum
            {
                open,
                closed,
                error,
                fetching0,
                fetching1,
                fetching2
            }

            itemIconModeEnum[] state;
            string href;

            public ItemIcon(string href)
            {
                this.state = new itemIconModeEnum[2];
                this.href = href;
            }

            public ItemIcon(itemIconModeEnum[] state, string href)
            {
                this.state = state;
                this.href = href;
            }

            public XNode ToXNode()
            {
                XElement xNode = new XElement("ItemIcon");

                string itemIconModes = "";
                foreach (itemIconModeEnum itemIconMode in this.state)
                {
                    if (itemIconMode != new itemIconModeEnum())
                    {
                        itemIconModes += itemIconMode.ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(itemIconModes))
                {
                    xNode.Add(new XElement("state",
                        new XText(itemIconModes.Substring(0,itemIconModes.Length - 1)))
                    );
                }
                if (!string.IsNullOrEmpty(this.href))
                {
                    xNode.Add(new XElement("href",
                        new XText(href))
                    );
                }

                return xNode;
            }
        }

        private ItemIcon[] itemIcons;

        public ListStyle(ItemIcon itemIcon)
        {
            this.itemIcons = new ItemIcon[] { itemIcon };
        }

        public ListStyle(ItemIcon[] itemIcons)
        {
            this.itemIcons = itemIcons;
        }

        public ListStyle(listItemTypeEnum listItemType, Color bgColor, ItemIcon[] itemIcons)
        {
            this.listItemType = listItemType;
            this.bgColor = bgColor;
            this.itemIcons = itemIcons;
        }

        public override XNode ToXNode()
        {
            XElement xNode = new XElement("ListStyle");

            if (this.listItemType != new listItemTypeEnum())
            {
                xNode.Add
                (
                    new XElement("listItemType",
                        new XText(this.listItemType.ToString())
                    )
                );
            }
            if (this.bgColor != new Color())
            {
                xNode.Add
                (
                    new XElement("bgColor",
                        new XText(this.ToHexString(bgColor))
                    )
                );
            }
            foreach (ItemIcon itemIcon in this.itemIcons)
            {
                xNode.Add(itemIcon.ToXNode());
            }

            return xNode;
        }
    }
}

// CLASSIFICATION: UNCLASSIFIED