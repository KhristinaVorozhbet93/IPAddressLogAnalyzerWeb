namespace IPAddressLogAnalyzer.WebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //плюс настроить это все так, чтобы происходило по времени
            //1. скачали все файлы c Minio, но настройки поменять здесь, они будут браться с другойго места

            //2. Фильтруем эти файлы сразу при считывании в MinioFileReaderService

            //3. Фильтруем с помощью IFilterService

            //4. 4. Записаем в базу с помощью AppDbContext



            //настройки клиента передавались извне
            //5. нужно чтобы была возможность завершения
            //6. чтобы не обрабатывать уже обрабтанные записи и файлы



        }
    }
}
