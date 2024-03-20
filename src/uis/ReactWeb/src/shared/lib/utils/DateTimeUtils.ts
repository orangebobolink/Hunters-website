export class DateTimeUtils {
    static formatTime(seconds: number) {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = Math.floor(seconds % 60);

        const formattedMinutes = minutes < 10 ? `0${minutes}` : `${minutes}`;
        const formattedSeconds = remainingSeconds < 10 ? `0${remainingSeconds}` : `${remainingSeconds}`;

        return `${formattedMinutes}:${formattedSeconds}`;
    }

    static dateAndTimeStringToTime(dateAndTime: string, locale: string) {
        const date = new Date(dateAndTime);

        return date.toLocaleString(locale, {
            hour: 'numeric',
            minute: 'numeric',
        });
    }

    static getNextDays(count: number) {
        const currentDate = new Date();
        const days = [];

        for (let i = 0; i < count; i += 1) {
            const nextDate = new Date(currentDate);
            nextDate.setDate(currentDate.getDate() + i);
            days.push({
                day: nextDate.getDate(),
                month: nextDate.getMonth() + 1,
            });
        }
        return days;
    }

    static formatDate(dateAndTime: string, locale: string) {
        const date = new Date(dateAndTime);

        const formatter = new Intl.DateTimeFormat(locale, {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
        });
        return formatter.format(date);
    }
}