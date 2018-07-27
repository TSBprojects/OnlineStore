using InternetStore.BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetStore.WEB.Models
{
    public class ProductModel
    {
        [Required]
        public int productId { get; set; }

        [MinLength(2, ErrorMessage = "Слишком короткое имя")]
        [MaxLength(40, ErrorMessage = "Слишком длинное имя")]
        [Required(ErrorMessage = "Введите название продукта")]
        [Display(Name = "Название продукта")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Слишком короткое частичное описание")]
        [MaxLength(300, ErrorMessage = "Слишком длинное частичное описание")]
        [Required(ErrorMessage = "Введите частичное описание")]
        [Display(Name = "Частичное описание")]
        public string PartialDescription { get; set; }

        [MinLength(2, ErrorMessage = "Слишком короткое описание")]
        [MaxLength(3000, ErrorMessage = "Слишком длинное описание")]
        [Required(ErrorMessage = "Введите описание")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Введите рейтинг")]
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Введите количество товара")]
        [Display(Name = "Количество товара")]
        public int ProductCount { get; set; }

        [Required(ErrorMessage = "Выберите категорию товара")]
        [Display(Name = "Категория товара")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Выберите теги товара")]
        [Display(Name = "Теги товара")]
        public string[] Tags { get; set; }

        [Required(ErrorMessage = "Выберите изображения товара")]
        [Display(Name = "Изображения товара")]
        public HttpFileCollectionBase ProductImages { get; set; }

        [Required(ErrorMessage = "Выберите, что сделать с изображениями")]
        [Display(Name = "Настройка загрузки изображений")]
        public string ImageSetting { get; set; }
    }
}