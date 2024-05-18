import {FormControl, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/shared/ui/select.tsx';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';

interface IProps {
    t: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang: string,
    options: string[]
}

const SelectForm = ({form, t, name, lang, options}:IProps) => {
    return (
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem>
                    <FormLabel>
                        {t(lang)}
                    </FormLabel>
                    <FormControl className="border-black/50">
                        <Select onValueChange={field.onChange} defaultValue={field.value}>
                            <SelectTrigger className="w-full">
                                <SelectValue placeholder={t(lang)} />
                            </SelectTrigger>
                            <SelectContent>
                                {options.map(o =><SelectItem value={o}>{o}</SelectItem>)}
                            </SelectContent>
                        </Select>
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )}
        />
    );
};

export default SelectForm;