﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RegistroGruposDetalle.Entidades
{
    public class Grupos
    {
        [Key]
        public int GrupoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Grupo { get; set; }
        public int Integrantes { get; set; }

        public virtual ICollection<GruposDetalle> Detalle { get; set; }

        public Grupos()
        {
            //Es obligatorio inicializar la lista
            this.Detalle = new List<GruposDetalle>();
        }

        /// Este metodo permite agretar un item a la lista
        /// No es obligatorio, lo creo por comodidad
        public void AgregarDetalle(int id, int GruposId, int PersonaId, string Cargo)
        {
            this.Detalle.Add(new GruposDetalle(id, GruposId, PersonaId, Cargo));
        }
    }
}
