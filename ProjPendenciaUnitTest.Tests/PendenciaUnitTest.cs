using Microsoft.EntityFrameworkCore;
using ProjPendenciaUnitTest.Api.Controllers;
using ProjPendenciaUnitTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjPendenciaUnitTest.Tests
{
    public class PendenciaUnitTest
    {
         private DbContextOptions<PendenciaContext>options;

        private void InitializeDataBase()
        {
            options = new DbContextOptionsBuilder<PendenciaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid()
            .ToString()).Options;

            using (var context = new PendenciaContext(options))
            {
                    context.Pendencia.Add(new Pendencia { Id = 1, Descricao= "Name 1", DataCriacao= DateTime.Now });
                    context.Pendencia.Add(new Pendencia { Id = 2, Descricao= "Name 2", DataCriacao= DateTime.Now });
                    context.Pendencia.Add(new Pendencia { Id = 3, Descricao= "Name 3", DataCriacao= DateTime.Now });
                
                    context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                IEnumerable<Pendencia> pendencias = pendenciaController.GetPendencia().Result.Value;
    
                Assert.Equal(3, pendencias.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                int pendenciaId = 2;
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pendencia = pendenciaController.GetPendencia(pendenciaId).Result.Value;
                Assert.Equal(2, pendencia.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

             Pendencia pendencia = new Pendencia()
            {
                Id = 4,
                Descricao= "Bolacha",
                DataCriacao = DateTime.Now

            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PostPendencia(pendencia).Result.Value;
                Assert.Equal(4, pend.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Pendencia pendencia = new Pendencia()
            {
                Id = 3,
                Descricao= "Bolacha",
                DataCriacao = DateTime.Now

            };

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                Pendencia pend = pendenciaController.PutPendencia(3, pendencia).Result.Value;
                Assert.Equal("Bolacha", pend.Descricao);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new PendenciaContext(options))
            {
                PendenciaController pendenciaController = new PendenciaController(context);
                 Pendencia pendencia = pendenciaController.DeletePendencia(2).Result.Value;
                Assert.Null(pendencia );
            }
        }
    }    
} 

   
              
