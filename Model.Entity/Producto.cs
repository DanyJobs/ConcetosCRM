﻿using System.ComponentModel.DataAnnotations;

namespace Model.Entity
{
    public class Producto
    {
        private string idProducto;
        private string nombre;
        public string Descripcion { get; set; }
        private double precioUnitario;
        private string categoria;
        private int estado;
        public string marca;
        public string bandaAncha;
        public int channels;
        //Para la parte de editar
        private decimal cantidad;
        private decimal descuento;
        [Display(Name = "Unidad de Medida:")]
        public string unidadMedida { get; set; }
        public string notas { get; set; }
        


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
        public decimal Cantidad
        {
            get
            {
                return cantidad;
            }
            set
            {
                cantidad = value;
            }
        }
        public decimal Descuento
        {
            get
            {
                return descuento;
            }
            set
            {
                descuento = value;
            }
        }

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

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        [Display(Name = "Precio Unitario:")]
        public double PrecioUnitario
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

        public string Categoria
        {
            get
            {
                return categoria;
            }

            set
            {
                categoria = value;
            }
        }


        public string Marca
        {
            get
            {
                return marca;
            }
            set
            {
                marca = value;
            }
        }


        [Display(Name = "Banda Ancha:")]
        public string BandaAncha
        {
            get
            {
                return bandaAncha;
            }
            set
            {
                bandaAncha = value;
            }
        }

        public int Channels
        {
            get
            {
                return channels;
            }
            set
            {
                channels = value;
            }
        }


        public Producto()
        {

        }
        public Producto(string idProducto)
        {
            this.idProducto = idProducto;
        }
        public Producto(string idProducto, string nombre, double precioUnitario, string categoria, string marca, string bandaAncha, int channels,string unidadMedida)
        {
            this.idProducto = idProducto;
            this.Nombre = nombre;
            this.PrecioUnitario = precioUnitario;
            this.Categoria = categoria;
            this.Marca = marca;
            this.BandaAncha = bandaAncha;
            this.Channels = channels;
            this.unidadMedida = unidadMedida;
        }
    }
}
