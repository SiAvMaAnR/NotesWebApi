using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Enums
{
    public enum TStatusCodes
    {
        /// <summary>
        /// Успешная обработка запроса
        /// </summary>
        OK = 200,
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
        Not_Modified = 304,
        /// <summary>
        /// Возникла проблема на стороне пользователя
        /// </summary>
        Bad_Request = 400,
        /// <summary>
        /// Запрет на просмотр страницы
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// Заданная страница (ресурс) больше не существует
        /// </summary>
        Not_Found = 404,
        /// <summary>
        /// Произошла ошибка на стороне сервера
        /// </summary>
        Internet_Server_Error = 500,
        /// <summary>
        /// Прокси сервер не может получить ответ от сайта
        /// </summary>
        Bad_Gateway = 502,
        /// <summary>
        /// Сервис запроса оказался перегружен и в данный момент не доступен
        /// </summary>
        Service_Unavailable = 503,
        /// <summary>
        /// Слишком долгий ответ
        /// </summary>
        Gateway_Timeout = 504
    }
}
