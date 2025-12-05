-- PostgreSQL Migration Script for Universal Business System
-- Run this script in PostgreSQL to create the database schema

-- ==================== CREATE DATABASE ====================
CREATE DATABASE UniversalBusinessDB
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.UTF-8'
    LC_CTYPE = 'en_US.UTF-8'
    CONNECTION LIMIT = -1;

-- Connect to the new database
\c UniversalBusinessDB;

-- ==================== ENABLE EXTENSIONS ====================
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ==================== CREATE TABLES ====================

-- 1. Organizations Table
CREATE TABLE IF NOT EXISTS organizations (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    email VARCHAR(200),
    phone VARCHAR(20),
    address TEXT,
    license_key VARCHAR(50),
    database_path VARCHAR(100),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. Roles Table
CREATE TABLE IF NOT EXISTS roles (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id),
    name VARCHAR(50) NOT NULL,
    description VARCHAR(200),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(organization_id, name)
);

-- 3. Permissions Table
CREATE TABLE IF NOT EXISTS permissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    key VARCHAR(50) NOT NULL UNIQUE,
    description VARCHAR(200),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 4. Role Permissions Table
CREATE TABLE IF NOT EXISTS role_permissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    role_id UUID NOT NULL REFERENCES roles(id) ON DELETE CASCADE,
    permission_id UUID NOT NULL REFERENCES permissions(id) ON DELETE CASCADE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(role_id, permission_id)
);

-- 5. Shop Types Table
CREATE TABLE IF NOT EXISTS shop_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    description TEXT,
    default_modules TEXT,
    default_units TEXT,
    default_categories TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 6. Units Table
CREATE TABLE IF NOT EXISTS units (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(50) NOT NULL,
    unit_type VARCHAR(30) NOT NULL,
    description TEXT,
    is_default BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 7. Organization Units Table
CREATE TABLE IF NOT EXISTS organization_units (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    unit_id UUID NOT NULL REFERENCES units(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(organization_id, unit_id)
);

-- 8. Modules Table
CREATE TABLE IF NOT EXISTS modules (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    key VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    version VARCHAR(20),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 9. Organization Modules Table
CREATE TABLE IF NOT EXISTS organization_modules (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    module_id UUID NOT NULL REFERENCES modules(id),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(organization_id, module_id)
);

-- 10. Categories Table
CREATE TABLE IF NOT EXISTS categories (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    parent_id UUID REFERENCES categories(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(organization_id, name)
);

-- 11. Users Table
CREATE TABLE IF NOT EXISTS users (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    username VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    password_salt VARCHAR(255) NOT NULL DEFAULT '',
    first_name VARCHAR(100),
    last_name VARCHAR(100),
    phone VARCHAR(20),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    last_login_at TIMESTAMP WITH TIME ZONE,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    is_email_verified BOOLEAN DEFAULT FALSE,
    email_verification_token VARCHAR(255),
    email_verification_expires TIMESTAMP WITH TIME ZONE,
    failed_login_attempts INTEGER DEFAULT 0,
    locked_until TIMESTAMP WITH TIME ZONE,
    organization_id UUID NOT NULL REFERENCES organizations(id),
    role_id UUID NOT NULL REFERENCES roles(id),
    UNIQUE(organization_id, username),
    UNIQUE(organization_id, email)
);

-- 12. Products Table
CREATE TABLE IF NOT EXISTS products (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    category_id UUID REFERENCES categories(id),
    name VARCHAR(150) NOT NULL,
    description TEXT,
    barcode VARCHAR(100),
    sku VARCHAR(50),
    cost_price DECIMAL(18,2),
    sale_price DECIMAL(18,2),
    opening_stock DECIMAL(18,3) DEFAULT 0,
    current_stock DECIMAL(18,3) DEFAULT 0,
    reorder_level DECIMAL(18,3) DEFAULT 0,
    unit_id UUID REFERENCES units(id),
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 13. Suppliers Table
CREATE TABLE IF NOT EXISTS suppliers (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    name VARCHAR(200) NOT NULL,
    contact_person VARCHAR(100),
    email VARCHAR(100),
    phone VARCHAR(20),
    address TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 14. Purchase Orders Table
CREATE TABLE IF NOT EXISTS purchase_orders (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    supplier_id UUID REFERENCES suppliers(id),
    order_no VARCHAR(50) NOT NULL,
    order_date DATE NOT NULL,
    expected_date DATE,
    total_amount DECIMAL(18,2) DEFAULT 0,
    discount DECIMAL(18,2) DEFAULT 0,
    tax DECIMAL(18,2) DEFAULT 0,
    net_amount DECIMAL(18,2) DEFAULT 0,
    status VARCHAR(20) DEFAULT 'Pending',
    notes TEXT,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(organization_id, order_no)
);

-- 15. Purchase Order Items Table
CREATE TABLE IF NOT EXISTS purchase_order_items (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    purchase_order_id UUID NOT NULL REFERENCES purchase_orders(id) ON DELETE CASCADE,
    product_id UUID NOT NULL REFERENCES products(id),
    quantity DECIMAL(18,3) NOT NULL,
    unit_price DECIMAL(18,2) NOT NULL,
    total_price DECIMAL(18,2) NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 16. Stock Adjustments Table
CREATE TABLE IF NOT EXISTS stock_adjustments (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    organization_id UUID NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
    product_id UUID NOT NULL REFERENCES products(id),
    adjustment_type VARCHAR(20) NOT NULL,
    quantity DECIMAL(18,3) NOT NULL,
    reason VARCHAR(200),
    reference_type VARCHAR(50),
    reference_id UUID,
    created_by UUID REFERENCES users(id),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ==================== CREATE INDEXES ====================
CREATE INDEX idx_users_username ON users(username);
CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_users_organization_id ON users(organization_id);
CREATE INDEX idx_organizations_license_key ON organizations(license_key);
CREATE INDEX idx_products_organization_id ON products(organization_id);
CREATE INDEX idx_products_category_id ON products(category_id);
CREATE INDEX idx_products_barcode ON products(barcode);
CREATE INDEX idx_products_sku ON products(sku);
CREATE INDEX idx_purchase_orders_organization_id ON purchase_orders(organization_id);
CREATE INDEX idx_purchase_orders_order_no ON purchase_orders(order_no);
CREATE INDEX idx_purchase_order_items_purchase_order_id ON purchase_order_items(purchase_order_id);
CREATE INDEX idx_purchase_order_items_product_id ON purchase_order_items(product_id);
CREATE INDEX idx_stock_adjustments_organization_id ON stock_adjustments(organization_id);
CREATE INDEX idx_stock_adjustments_product_id ON stock_adjustments(product_id);
CREATE INDEX idx_categories_organization_id ON categories(organization_id);
CREATE INDEX idx_suppliers_organization_id ON suppliers(organization_id);

-- ==================== INSERT DEFAULT DATA ====================

-- Insert default permissions
INSERT INTO permissions (name, key, description) VALUES
('View Dashboard', 'dashboard.view', 'View dashboard and reports'),
('Manage Products', 'products.manage', 'Create, edit, and delete products'),
('View Products', 'products.view', 'View product list and details'),
('Manage Sales', 'sales.manage', 'Create and manage sales'),
('View Sales', 'sales.view', 'View sales reports and history'),
('Manage Purchases', 'purchases.manage', 'Create and manage purchase orders'),
('View Purchases', 'purchases.view', 'View purchase orders and history'),
('Manage Inventory', 'inventory.manage', 'Manage stock and adjustments'),
('View Inventory', 'inventory.view', 'View inventory reports'),
('Manage Users', 'users.manage', 'Create, edit, and delete users'),
('View Users', 'users.view', 'View user list and details'),
('Manage Settings', 'settings.manage', 'Manage system settings'),
('View Reports', 'reports.view', 'View all reports'),
('Manage Organization', 'organization.manage', 'Manage organization details'),
('System Admin', 'system.admin', 'Full system administration');

-- Insert default modules
INSERT INTO modules (name, key, description, version) VALUES
('Dashboard', 'dashboard', 'Main dashboard with overview and reports', '1.0.0'),
('Sales', 'sales', 'Sales management and point of sale', '1.0.0'),
('Inventory', 'inventory', 'Inventory and stock management', '1.0.0'),
('Purchases', 'purchases', 'Purchase order management', '1.0.0'),
('Reports', 'reports', 'Business reports and analytics', '1.0.0'),
('Settings', 'settings', 'System settings and configuration', '1.0.0'),
('User Management', 'users', 'User and role management', '1.0.0');

-- Insert Shop Types
INSERT INTO shop_types (name, description, default_units, default_categories) VALUES
('Grocery Store', 'Sell food items, vegetables, fruits, dairy, and household items', '["Piece", "Kilogram", "Gram", "Liter", "Milliliter", "Packet", "Box", "Dozen"]', '["Fresh Vegetables", "Fresh Fruits", "Meat & Poultry", "Seafood", "Dairy & Eggs", "Snacks", "Beverages", "Cooking Oil", "Spices"]'),
('Pharmacy', 'Sell medicines, medical supplies, and health products', '["Strip", "Tablet", "Capsule", "Bottle", "Tube", "Milliliter", "Milligram"]', '["Tablets", "Capsules", "Syrups", "Injections", "Medical Devices", "Bandages", "Ointments", "Vitamins"]'),
('Petrol Pump', 'Sell fuel, lubricants, and car accessories', '["Liter", "Gallon", "Barrel"]', '["Petrol", "Diesel", "Lubricants", "Car Accessories"]'),
('Electronics', 'Sell electronic devices and accessories', '["Piece", "Box", "Set"]', '["Mobile Phones", "Laptops", "Accessories", "Appliances"]'),
('Clothing', 'Sell garments, shoes, and fashion accessories', '["Piece", "Pair", "Set", "Box"]', '["Men''s Clothing", "Women''s Clothing", "Kids Clothing", "Shoes", "Accessories"]'),
('Bakery', 'Sell baked goods, sweets, and confectionery', '["Piece", "Kilogram", "Gram", "Box", "Packet"]', '["Bread", "Cakes", "Pastries", "Sweets", "Cookies"]'),
('Hardware', 'Sell construction materials, tools, and hardware items', '["Piece", "Kilogram", "Meter", "Box", "Bag"]', '["Tools", "Paints", "Electrical", "Plumbing", "Building Materials"]'),
('Furniture', 'Sell furniture items', '["Piece", "Set"]', '["Living Room", "Bedroom", "Office", "Dining"]'),
('Mobile Accessories', 'Sell phone cases, chargers, and accessories', '["Piece", "Box", "Packet"]', '["Phone Cases", "Chargers", "Screen Protectors", "Earphones"]'),
('Bookshop', 'Sell books, stationery, and educational materials', '["Piece", "Box", "Set"]', '["Books", "Stationery", "Educational", "Magazines"]'),
('Meat Shop', 'Sell meat and poultry products', '["Kilogram", "Gram", "Piece"]', '["Chicken", "Mutton", "Beef", "Processed Meat"]'),
('Cosmetics', 'Sell beauty and personal care products', '["Piece", "Bottle", "Tube", "Packet"]', '["Skincare", "Makeup", "Hair Care", "Fragrances"]'),
('Auto Parts', 'Sell automobile spare parts and accessories', '["Piece", "Box", "Set"]', '["Engine Parts", "Body Parts", "Electrical", "Accessories"]');

-- Insert Units
INSERT INTO units (name, unit_type, description, is_default) VALUES
-- Quantity
('Piece', 'quantity', 'Single item', TRUE),
('Packet', 'quantity', 'Pack of items', FALSE),
('Box', 'quantity', 'Box of items', FALSE),
('Carton', 'quantity', 'Carton of items', FALSE),
('Dozen', 'quantity', '12 pieces', FALSE),
('Pair', 'quantity', '2 pieces', FALSE),
('Set', 'quantity', 'Set of items', FALSE),
('Bundle', 'quantity', 'Bundle of items', FALSE),
('Roll', 'quantity', 'Roll of items', FALSE),
('Strip', 'quantity', 'Strip of tablets', FALSE),
('Tube', 'quantity', 'Tube of items', FALSE),
('Kit', 'quantity', 'Kit of items', FALSE),

-- Weight
('Gram', 'weight', 'Gram weight', FALSE),
('Kilogram', 'weight', 'Kilogram weight', TRUE),
('Milligram', 'weight', 'Milligram weight', FALSE),
('Ton', 'weight', 'Metric ton', FALSE),
('Ounce', 'weight', 'Ounce weight', FALSE),
('Pound', 'weight', 'Pound weight', FALSE),

-- Volume
('Milliliter', 'volume', 'Milliliter volume', FALSE),
('Liter', 'volume', 'Liter volume', TRUE),
('Gallon', 'volume', 'Gallon volume', FALSE),
('Barrel', 'volume', 'Barrel volume', FALSE),
('Drum', 'volume', 'Drum volume', FALSE),
('Can', 'volume', 'Can volume', FALSE),
('Bottle', 'volume', 'Bottle volume', FALSE),

-- Length
('Meter', 'length', 'Meter length', TRUE),
('Centimeter', 'length', 'Centimeter length', FALSE),
('Millimeter', 'length', 'Millimeter length', FALSE),
('Foot', 'length', 'Foot length', FALSE),
('Inch', 'length', 'Inch length', FALSE),
('Yard', 'length', 'Yard length', FALSE);

-- ==================== CREATE FUNCTIONS ====================

-- Function to update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';

-- ==================== CREATE TRIGGERS ====================

-- Trigger for organizations table
CREATE TRIGGER update_organizations_updated_at BEFORE UPDATE ON organizations
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Trigger for users table
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON users
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Trigger for products table
CREATE TRIGGER update_products_updated_at BEFORE UPDATE ON products
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Trigger for suppliers table
CREATE TRIGGER update_suppliers_updated_at BEFORE UPDATE ON suppliers
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Trigger for purchase_orders table
CREATE TRIGGER update_purchase_orders_updated_at BEFORE UPDATE ON purchase_orders
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- ==================== GRANT PERMISSIONS ====================
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO postgres;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO postgres;

-- ==================== VERIFICATION QUERIES ====================
SELECT 'PostgreSQL database schema created successfully!' as message;
SELECT COUNT(*) as organizations_count FROM organizations;
SELECT COUNT(*) as shop_types_count FROM shop_types;
SELECT COUNT(*) as units_count FROM units;
SELECT COUNT(*) as modules_count FROM modules;
SELECT COUNT(*) as permissions_count FROM permissions;
