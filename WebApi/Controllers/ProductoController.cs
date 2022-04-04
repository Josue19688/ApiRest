using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class ProductoController : BaseApiController
    {

        //private readonly IProductoRepository _productoRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        /* http://localhost:61600/api/Producto */
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var spec = new ProductoWithCategoriaAndMarcaSpecification();
            var productos = await _productoRepository.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Producto>,IReadOnlyList<ProductoDto>>(productos));
        }

        /* http://localhost:61600/api/Producto/1 */
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {//deve incluir la logica de la ocndicion del query y tambien la relaciones entre entidades

            var spec = new ProductoWithCategoriaAndMarcaSpecification(id);
            var producto =  await _productoRepository.GetByIdWithSpec(spec);
            if (producto == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }
            return _mapper.Map<Producto, ProductoDto>(producto);
        }


        [HttpPost]
        public async Task<ActionResult<Producto>>Post(Producto producto)
        {
            var resultado= await _productoRepository.Add(producto);
            if (resultado == 0)
            {
                throw new Exception("No se inserto el producto");
            }

            return Ok(producto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>>Put(int id, Producto producto)
        {
            producto.Id = id;
            var resultado = await _productoRepository.Update(producto);
            if (resultado == 0)
            {
                throw new Exception("No se pudo actualizar el registro");
            }
            return Ok(producto);
        }
    }
}
