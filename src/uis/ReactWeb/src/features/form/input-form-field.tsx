import {FC} from 'react';
import {FormControl, FormField, FormItem, FormLabel, FormMessage, Input} from '@/shared/ui';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';

interface IProps {
    t: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang: string,
    type?: string
}

const InputFormField : FC<IProps> = ({t, form, name, lang, type}) => {
    return (
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem>
                    <FormLabel>
                        {t(lang)}
                    </FormLabel>
                    <FormControl>
                    {
                            {
                                "number": <Input type={type}
                                                 {...field}
                                                 onChange={event => field.onChange(+event.target.value)} />,
                                "file":<Input type="url" placeholder="http://image"/>,
                            }[type!] || <Input type={type}
                            {...field}/>
                        }
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )}
        />
    );
};

export default InputFormField;