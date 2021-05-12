﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCompany.model
{
    public interface IRepController
    {
        Employee GetEmployeeFromDB(string sorname, string phone);
        ObservableCollection<ProductPosition> GetStorageProducts(Center center);
        ObservableCollection<Require> GetDBRequiersFrom(Center center);
        ObservableCollection<Require> GetDBRequiersTo(Center center);
        ObservableCollection<ProductPosition> GetDBCenterProductsPosition(Center center);
        void UpdateProductInDataBase(Product product);
        void UpdateProductPositionInDB(ProductPosition product);
        void DeleteProductPositionFromDB(ProductPosition product);
        void AddProductPositionInDB(ProductPosition product);
        void AddProductInDB(Product product);
        ObservableCollection<Product> GetDBProducts();

    }
}