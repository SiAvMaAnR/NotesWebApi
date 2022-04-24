using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Enums
{
    public enum TStatusCode
    {
        /// <summary>
        /// Успешная обработка запроса
        /// </summary>
        OK = 200,
        /// <summary>
        /// Успешное создание объекта
        /// </summary>
        Created = 201,
        /// <summary>
        /// Запрос прошел успешно, без контента
        /// </summary>
        NoContent = 204,
        /// <summary>
        /// URL страницы изменен
        /// </summary>
        Moved_Permanently = 301,
        /// <summary>
        /// Cтраница временно недоступна по данному адресу, но у нее есть новый временный URL.
        /// </summary>
        Found = 302,
        /// <summary>
        /// На запрашиваемой странице не было обновлений с момента последнего ее посещения
        /// </summary>
        NotModified = 304,
        /// <summary>
        /// Возникла проблема на стороне пользователя
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Возникла проблема с аутентификацией или авторизацией
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// Запрет на просмотр страницы
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// Заданная страница (ресурс) больше не существует
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// Произошла ошибка на стороне сервера
        /// </summary>
        InternetServerError = 500,
        /// <summary>
        /// Прокси сервер не может получить ответ от сайта
        /// </summary>
        BadGateway = 502,
        /// <summary>
        /// Сервис запроса оказался перегружен и в данный момент не доступен
        /// </summary>
        ServiceUnavailable = 503,
        /// <summary>
        /// Слишком долгий ответ
        /// </summary>
        GatewayTimeout = 504
    }
}
