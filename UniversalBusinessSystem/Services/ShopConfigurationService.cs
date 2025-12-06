using System;
using System.Collections.Generic;
using System.Linq;
using UniversalBusinessSystem.Core.Entities;

namespace UniversalBusinessSystem.Services
{
    public class ShopConfiguration
    {
        public string ShopType { get; set; } = string.Empty;
        public List<Unit> Units { get; set; } = new();
        public List<string> Categories { get; set; } = new();
    }

    public class ShopConfigurationService
    {
        private readonly Dictionary<string, ShopConfiguration> _shopConfigurations;

        public ShopConfigurationService()
        {
            _shopConfigurations = InitializeShopConfigurations();
        }

        public List<ShopType> GetShopTypes()
        {
            return _shopConfigurations.Keys.Select(name => new ShopType 
            { 
                Name = name,
                Description = GetShopTypeDescription(name)
            }).ToList();
        }

        public ShopConfiguration GetShopConfiguration(string shopType)
        {
            return _shopConfigurations.TryGetValue(shopType, out var config) ? config : new ShopConfiguration();
        }

        private string GetShopTypeDescription(string shopType)
        {
            return shopType switch
            {
                "Grocery Store" => "Sell food items, vegetables, fruits, dairy, and household items",
                "Pharmacy" => "Sell medicines, medical supplies, and health products",
                "Petrol Pump" => "Sell fuel, lubricants, and car accessories",
                "Electronics" => "Sell electronic devices and accessories",
                "Clothing" => "Sell garments, shoes, and fashion accessories",
                "Bakery" => "Sell baked goods, sweets, and confectionery",
                "Hardware" => "Sell construction materials, tools, and hardware items",
                "Furniture" => "Sell furniture items",
                "Mobile Accessories" => "Sell phone cases, chargers, and accessories",
                "Bookshop" => "Sell books, stationery, and educational materials",
                "Meat Shop" => "Sell meat and poultry products",
                "Cosmetics" => "Sell beauty and personal care products",
                "Auto Parts" => "Sell automobile spare parts and accessories",
                _ => "General business type"
            };
        }

        private Dictionary<string, ShopConfiguration> InitializeShopConfigurations()
        {
            var configs = new Dictionary<string, ShopConfiguration>();

            // Universal Units
            var universalUnits = new List<Unit>
            {
                new() { Name = "Piece", Type = "quantity", IsDefault = true },
                new() { Name = "Packet", Type = "quantity", IsDefault = false },
                new() { Name = "Box", Type = "quantity", IsDefault = false },
                new() { Name = "Carton", Type = "quantity", IsDefault = false },
                new() { Name = "Dozen", Type = "quantity", IsDefault = false },
                new() { Name = "Pair", Type = "quantity", IsDefault = false },
                new() { Name = "Set", Type = "quantity", IsDefault = false },
                new() { Name = "Kilogram", Type = "weight", IsDefault = false },
                new() { Name = "Gram", Type = "weight", IsDefault = false },
                new() { Name = "Liter", Type = "volume", IsDefault = false },
                new() { Name = "Milliliter", Type = "volume", IsDefault = false },
                new() { Name = "Meter", Type = "length", IsDefault = false },
                new() { Name = "Service Fee", Type = "service", IsDefault = false }
            };

            // Grocery Store
            configs["Grocery Store"] = new ShopConfiguration
            {
                ShopType = "Grocery Store",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Kilogram", "Gram", "Liter", "Milliliter", "Packet", "Box", "Dozen" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Fresh Vegetables", "Fresh Fruits", "Meat & Poultry", "Seafood", "Dairy & Eggs",
                    "Snacks", "Beverages", "Cooking Oil", "Spices", "Grains", "Bakery Items",
                    "Frozen Foods", "Cleaning Supplies", "Toiletries", "Baby Products"
                }
            };

            // Pharmacy
            configs["Pharmacy"] = new ShopConfiguration
            {
                ShopType = "Pharmacy",
                Units = universalUnits.Where(u => 
                    new[] { "Strip", "Tablet", "Capsule", "Bottle", "Tube", "Milliliter", "Milligram", "Piece" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Tablets", "Capsules", "Syrups", "Injections", "Medical Devices", "Bandages",
                    "Ointments", "Vitamins", "First Aid", "Personal Care", "Baby Care"
                }
            };

            // Petrol Pump
            configs["Petrol Pump"] = new ShopConfiguration
            {
                ShopType = "Petrol Pump",
                Units = universalUnits.Where(u => 
                    new[] { "Liter", "Gallon", "Barrel", "Service Fee" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Petrol", "Diesel", "Lubricants", "Car Accessories", "Services"
                }
            };

            // Electronics
            configs["Electronics"] = new ShopConfiguration
            {
                ShopType = "Electronics",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Box", "Set", "Service Fee" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Mobile Phones", "Laptops", "Accessories", "Appliances", "Gaming", "Audio"
                }
            };

            // Clothing
            configs["Clothing"] = new ShopConfiguration
            {
                ShopType = "Clothing",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Pair", "Set", "Box" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Men's Clothing", "Women's Clothing", "Kids Clothing", "Shoes", "Accessories"
                }
            };

            // Bakery
            configs["Bakery"] = new ShopConfiguration
            {
                ShopType = "Bakery",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Kilogram", "Gram", "Box", "Packet" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Bread", "Cakes", "Pastries", "Sweets", "Cookies", "Beverages"
                }
            };

            // Hardware
            configs["Hardware"] = new ShopConfiguration
            {
                ShopType = "Hardware",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Kilogram", "Meter", "Box", "Bag" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Tools", "Paints", "Electrical", "Plumbing", "Building Materials", "Fasteners"
                }
            };

            // Furniture
            configs["Furniture"] = new ShopConfiguration
            {
                ShopType = "Furniture",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Set" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Living Room", "Bedroom", "Office", "Dining", "Outdoor"
                }
            };

            // Mobile Accessories
            configs["Mobile Accessories"] = new ShopConfiguration
            {
                ShopType = "Mobile Accessories",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Box", "Packet" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Phone Cases", "Chargers", "Screen Protectors", "Earphones", "Cables"
                }
            };

            // Bookshop
            configs["Bookshop"] = new ShopConfiguration
            {
                ShopType = "Bookshop",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Box", "Set" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Books", "Stationery", "Educational", "Magazines", "Office Supplies"
                }
            };

            // Meat Shop
            configs["Meat Shop"] = new ShopConfiguration
            {
                ShopType = "Meat Shop",
                Units = universalUnits.Where(u => 
                    new[] { "Kilogram", "Gram", "Piece" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Chicken", "Mutton", "Beef", "Pork", "Processed Meat", "Seafood"
                }
            };

            // Cosmetics
            configs["Cosmetics"] = new ShopConfiguration
            {
                ShopType = "Cosmetics",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Bottle", "Tube", "Packet", "Milliliter", "Gram" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Skincare", "Makeup", "Hair Care", "Fragrances", "Personal Care"
                }
            };

            // Auto Parts
            configs["Auto Parts"] = new ShopConfiguration
            {
                ShopType = "Auto Parts",
                Units = universalUnits.Where(u => 
                    new[] { "Piece", "Box", "Set", "Liter" }.Contains(u.Name)).ToList(),
                Categories = new List<string>
                {
                    "Engine Parts", "Body Parts", "Electrical", "Accessories", "Fluids"
                }
            };

            return configs;
        }
    }
}
