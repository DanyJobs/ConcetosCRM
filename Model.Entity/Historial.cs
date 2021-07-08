﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Historial
    {
        private string idProducto;
        private string cliente;
        private string numCotizacion;
        private string producto;
        private string precioUnitario;
        private int estado;


        public string IdProducto
        {
            get
            {
                return idProducto;
            }

            set
            {
                idProducto = value;
            }
        }

        public string Cliente
        {
            get
            {
                return cliente;
            }

            set
            {
                cliente = value;
            }
        }

        public string NumCotizacion
        {
            get
            {
                return numCotizacion;
            }

            set
            {
                numCotizacion = value;
            }
        }

        public string Producto
        {
            get
            {
                return producto;
            }

            set
            {
                producto = value;
            }
        }
        public string PrecioUnitario
        {
            get
            {
                return precioUnitario;
            }

            set
            {
                precioUnitario = value;
            }
        }

        public int Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }
        public Historial()
        {

        }
        public Historial(string idProducto,string cliente , string numCotizacion, string producto, string precioUnitario)
        {
            this.idProducto = idProducto;
            this.cliente = cliente;
            this.numCotizacion = numCotizacion;
            this.producto = producto;
            this.precioUnitario = precioUnitario;
        }
        public Historial(string producto)
        {
            this.producto = producto;
        }
    }
}