using DataAccess.Abstract;
using DataAccess.Concretes.EntityFramework;
using Northwind.Business.Abstract;
using Northwind.Business.Concretes;
using Northwind.Entities.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IProductDal efProductDal = new EfProductDal();
        ICategoryDal efCategoryDal = new EfCategoryDal();
        IProductService productService = new ProductManager(new EfProductDal());

        private void Form1_Load(object sender, EventArgs e)
        {

            GetProducts();
            GetCategories(cbxCategory);
            GetCategories(cbxCategory2);

        }
        private void GetProducts()
        {
            IProductService productManager = new ProductManager(efProductDal);
            dgwProduct.DataSource = productManager.GetProducts();
        }
        private void GetCategories(ComboBox componentName)
        {
            ICategoryService categoryManager = new CategoryManager(efCategoryDal);
            componentName.DataSource = categoryManager.GetCategories();
            componentName.DisplayMember = "CategoryName";
            componentName.ValueMember = "CategoryID";
        }
        private void GetProductsByCategory()
        {
            try
            {
                dgwProduct.DataSource = productService.GetProductsByCategoryId(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {


            }
        }
        private void GetProductsByProductName()
        {

            string keyProductName = tbxProductName.Text;

            if (String.IsNullOrEmpty(keyProductName))
            {
                GetProducts();
            }
            else
            {
                dgwProduct.DataSource = productService.GetProductsByProductName(keyProductName);

            }


        }


        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetProductsByCategory();



        }
        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            GetProductsByProductName();

        }
        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowProductInformation();

        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddProduct();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }


        private void AddProduct()
        {
            try
            {
                productService.AddProduct(new Product()
                {
                    ProductName = txtProductName2.Text,
                    CategoryID = Convert.ToInt32(cbxCategory2.SelectedValue),
                    UnitPrice = Convert.ToDecimal(txtPrice.Text),
                    UnitsInStock = Convert.ToInt16(txtStock.Text),
                    QuantityPerUnit = txtQuantityPerUnit.Text

                });
                MessageBox.Show("Product added.");
                GetProducts();
            }
            catch
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz");

            }








        }
        private void UpdateProduct()
        {


            productService.UpdateProduct(new Product
            {
                ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                ProductName = txtProductName2.Text,
                CategoryID = Convert.ToInt32((cbxCategory2.SelectedValue)),
                UnitPrice = Convert.ToDecimal(txtPrice.Text),
                UnitsInStock = Convert.ToInt16(txtStock.Text),
                QuantityPerUnit = txtQuantityPerUnit.Text


            });


            GetProducts();
            MessageBox.Show("Product updated.");

        }
        private void DeleteProduct()
        {
            if (dgwProduct.CurrentRow != null)
            {
                productService.DeleteProduct((new Product
                {
                    ProductID = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                }));
                MessageBox.Show("Product deleted.");
                GetProducts();
            }

        }
        private void ShowProductInformation()
        {

            txtProductName2.Text = dgwProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategory2.SelectedValue = dgwProduct.CurrentRow.Cells[2].Value;
            txtPrice.Text = dgwProduct.CurrentRow.Cells[3].Value.ToString();
            txtStock.Text = dgwProduct.CurrentRow.Cells[4].Value.ToString();
            txtQuantityPerUnit.Text = dgwProduct.CurrentRow.Cells[5].Value.ToString();

        }


    }
}
