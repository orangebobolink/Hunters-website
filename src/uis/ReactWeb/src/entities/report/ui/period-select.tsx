import {Select, SelectContent, SelectGroup, SelectItem, SelectTrigger, SelectValue} from '@/shared/ui/select.tsx';
import {Period} from '@/entities/report/models/Period.ts';

interface IProps {
    period:Period,
    setPeriod:(period:Period) => void
}

const PeriodSelect = ({period, setPeriod}:IProps) => {
    const handlePeriodChange = (value: string) => {
        const selectedPeriod: Period = Period[value as keyof typeof Period];
        setPeriod(selectedPeriod);
    };

    return (
        <Select onValueChange={handlePeriodChange} defaultValue={period.toString()}>
            <SelectTrigger className="w-[200px] mb-5">
                <SelectValue className="text-white"  />
            </SelectTrigger>
            <SelectContent>
                <SelectGroup>
                    <SelectItem value={Period.Week.toString()}>Неделя</SelectItem>
                    <SelectItem value={Period.Month.toString()}>Месяц</SelectItem>
                    <SelectItem value={Period.Year.toString()}>Год</SelectItem>
                    <SelectItem value={Period.Ever.toString()}>За все время</SelectItem>
                </SelectGroup>
            </SelectContent>
        </Select>
    );
};

export default PeriodSelect;