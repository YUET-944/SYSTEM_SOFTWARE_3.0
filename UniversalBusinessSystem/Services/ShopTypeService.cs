using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using UniversalBusinessSystem.Core.Entities;
using UniversalBusinessSystem.Data;

namespace UniversalBusinessSystem.Services;

public class ShopTypeService
{
    private readonly IServiceProvider _serviceProvider;

    public ShopTypeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ShopConfiguration GetConfiguration(string shopTypeName)
    {
        var configs = GetShopConfigurations();
        
        if (configs.TryGetValue(shopTypeName, out var config))
        {
            return config;
        }
        
        // Return default configuration if not found
        return new ShopConfiguration
        {
            Units = new List<string> { "piece", "kg", "L", "box" },
            Categories = new List<string> { "General", "Miscellaneous" },
            DefaultUnit = "piece"
        };
    }

    public async Task<List<ShopType>> GetShopTypesAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UniversalBusinessSystemDbContext>();
        
        return await context.ShopTypes
            .OrderBy(st => st.SortOrder)
            .ToListAsync();
    }

    public async Task<ShopConfiguration?> GetConfigurationByIdAsync(Guid shopTypeId)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UniversalBusinessSystemDbContext>();
        
        var shopType = await context.ShopTypes.FindAsync(shopTypeId);
        if (shopType == null) return null;

        return new ShopConfiguration
        {
            Units = ParseJsonList(shopType.DefaultUnits),
            Categories = ParseJsonList(shopType.DefaultModules), // Using modules as categories for now
            DefaultUnit = "piece"
        };
    }

    private Dictionary<string, ShopConfiguration> GetShopConfigurations()
    {
        return new Dictionary<string, ShopConfiguration>
        {
            {
                "General Store",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "kg", "g", "L", "ml", "box", "dozen", "packet", "meter", "cm" },
                    Categories = new List<string>
                    {
                        "Groceries", "Electronics", "Clothing", "Home & Garden", 
                        "Toys", "Books", "Sports", "Health & Beauty", "Automotive"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Grocery",
                new ShopConfiguration
                {
                    Units = new List<string> { "kg", "g", "L", "ml", "piece", "packet", "dozen", "box" },
                    Categories = new List<string>
                    {
                        "Fresh Vegetables", "Fresh Fruits", "Meat & Poultry", "Seafood",
                        "Dairy & Eggs", "Bakery", "Snacks", "Beverages", "Canned Goods",
                        "Frozen Foods", "Spices & Condiments", "Personal Care"
                    },
                    DefaultUnit = "kg"
                }
            },
            {
                "Pharmacy",
                new ShopConfiguration
                {
                    Units = new List<string> { "strip", "tablet", "capsule", "bottle", "tube", "ml", "mg", "g", "box", "vial" },
                    Categories = new List<string>
                    {
                        "Prescription Drugs", "OTC Medicines", "Vitamins & Supplements",
                        "Medical Devices", "Personal Care", "Baby Care", "First Aid",
                        "Health Foods", "Beauty Products"
                    },
                    DefaultUnit = "strip"
                }
            },
            {
                "Electronics",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "box", "set", "kit", "meter", "cm" },
                    Categories = new List<string>
                    {
                        "Mobile Phones", "Computers", "TV & Audio", "Cameras",
                        "Gaming", "Accessories", "Appliances", "Smart Home",
                        "Batteries", "Cables & Adapters"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Clothing/Boutique",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "set", "pair", "box" },
                    Categories = new List<string>
                    {
                        "Men's Wear", "Women's Wear", "Kids Wear", "Footwear",
                        "Accessories", "Bags", "Jewelry", "Sportswear",
                        "Formal Wear", "Casual Wear"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Mobile Shop",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "box", "set", "kit" },
                    Categories = new List<string>
                    {
                        "Mobile Phones", "Tablets", "Accessories", "Cases",
                        "Screen Protectors", "Chargers", "Headphones", "Smart Watches",
                        "Power Banks", "Memory Cards"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Hardware",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "box", "set", "kg", "meter", "cm", "liter", "bag" },
                    Categories = new List<string>
                    {
                        "Tools", "Fasteners", "Paints", "Electrical",
                        "Plumbing", "Building Materials", "Safety Equipment",
                        "Garden Tools", "Hardware", "Locks & Keys"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Petrol Pump",
                new ShopConfiguration
                {
                    Units = new List<string> { "liter", "gallon", "piece", "box" },
                    Categories = new List<string>
                    {
                        "Petrol", "Diesel", "LPG", "Lubricants",
                        "Car Accessories", "Snacks & Drinks", "Car Care",
                        "Emergency Items", "Services"
                    },
                    DefaultUnit = "liter"
                }
            },
            {
                "Bakery",
                new ShopConfiguration
                {
                    Units = new List<string> { "kg", "g", "piece", "box", "dozen", "packet" },
                    Categories = new List<string>
                    {
                        "Breads", "Cakes", "Pastries", "Cookies",
                        "Savory Items", "Gluten Free", "Custom Orders",
                        "Ingredients", "Beverages"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Furniture",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "set", "box" },
                    Categories = new List<string>
                    {
                        "Living Room", "Bedroom", "Dining", "Office",
                        "Outdoor", "Mattresses", "Storage", "Lighting",
                        "Decor", "Kids Furniture"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Bookshop",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "set", "box" },
                    Categories = new List<string>
                    {
                        "Fiction", "Non-Fiction", "Academic", "Children",
                        "Stationery", "Magazines", "Comics", "Educational",
                        "Rare Books", "Gift Items"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Meat Shop",
                new ShopConfiguration
                {
                    Units = new List<string> { "kg", "g", "piece", "packet", "box" },
                    Categories = new List<string>
                    {
                        "Fresh Meat", "Poultry", "Seafood", "Processed Meat",
                        "Frozen Items", "Marinated Items", "Special Cuts",
                        "Organic", "Exotic Meats"
                    },
                    DefaultUnit = "kg"
                }
            },
            {
                "Cosmetics",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "ml", "g", "box", "set", "tube", "bottle" },
                    Categories = new List<string>
                    {
                        "Skincare", "Makeup", "Hair Care", "Fragrances",
                        "Personal Care", "Men's Grooming", "Natural Products",
                        "Luxury Brands", "Gift Sets"
                    },
                    DefaultUnit = "piece"
                }
            },
            {
                "Auto Parts",
                new ShopConfiguration
                {
                    Units = new List<string> { "piece", "set", "box", "kit", "liter", "kg" },
                    Categories = new List<string>
                    {
                        "Engine Parts", "Transmission", "Brakes", "Suspension",
                        "Electrical", "Body Parts", "Tires & Wheels",
                        "Fluids & Chemicals", "Tools", "Accessories"
                    },
                    DefaultUnit = "piece"
                }
            }
        };
    }

    private List<string> ParseJsonList(string json)
    {
        try
        {
            if (string.IsNullOrEmpty(json))
                return new List<string>();
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }
}

public class ShopConfiguration
{
    public List<string> Units { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    public string DefaultUnit { get; set; } = "piece";
}
