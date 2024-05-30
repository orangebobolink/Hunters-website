export class EnumService {
    static statysTranslate = (status: string) => {
        switch (status) {
            case 'Compiled':
                return 'Создан';
            case 'Given':
                return 'Выдан';
            case 'Rented':
                return 'Арендуется';
            case 'Pending':
                return 'Рассматривается';
            case 'Returned':
                return 'Возвращен';
            case 'Cancelled':
                return 'Закрыт';
            case 'Recived':
                return 'Получен';
            case 'Buyed':
                return 'Куплен';
            case 'Completed':
                return 'Закрыт';
            default:
                return 'Неизвестный';
        }
    };

    static animalTranslate = (status: string) => {
        switch (status) {
            case 'Mammal':
                return 'Млекопитающие';
            case 'Bird':
                return 'Птица';
            default:
                return 'Неизвестный';
        }
    };
}
