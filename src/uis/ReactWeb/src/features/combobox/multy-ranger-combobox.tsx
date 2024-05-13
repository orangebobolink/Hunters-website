import {Listbox, Transition} from '@headlessui/react';
import {CaretSortIcon, CheckIcon} from '@radix-ui/react-icons';
import {Dispatch, Fragment, SetStateAction, useEffect, useState} from 'react';
import {FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';
import {User} from '@/entities/user/models/User.ts';
import {UserService} from '@/entities/user/api/UserService.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';


interface IProps {
    t: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang: string,
    setParticipant: Dispatch<SetStateAction<User[]>>
}

const MultyRangerCombobox = ({
                                 t,
                                 form,
                                 name,
                                 lang, setParticipant
}:IProps) => {
    const {id} = useAppSelector(selectAuth);
    const [selected, setSelected] = useState<User[]>([]);
    const [options, setOptions] = useState<User[]>([]);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const rangers = await UserService.getAllRangers();
                setOptions(rangers.data);
                setSelected(prev => [...prev, ...rangers.data.filter(u=> u.id == id)!])
                setParticipant((prev) => [
                    ...prev,
                    ...rangers.data.filter((u) => u.id === id)!
                ]);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, []);

    useEffect(() => {
        form.setValue(name, selected);
        setParticipant(selected);
    }, [selected]);

    return (
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem className="flex flex-col">
                    <FormLabel>{t(lang)}</FormLabel>
                    <Listbox
                        value={selected}
                        onChange={setSelected}
                        multiple>
                        <div className='relative'>
                            <Listbox.Button className='flex h-9 w-full items-center justify-between
                            rounded-md border border-input bg-transparent px-3 py-2 text-sm shadow-sm
                            ring-offset-background placeholder:text-muted-foreground focus:outline-none
                            focus:ring-1 focus:ring-ring disabled:cursor-not-allowed disabled:opacity-50'>
                                <span className='block truncate'>
                                    {
                                        selected?.map(option => UserService.getLastAndFirstName(option)).join(', ')
                                    }
                                </span>
                                <CaretSortIcon className='h-4 w-4 opacity-50' />
                            </Listbox.Button>
                            <Transition
                                as={Fragment}
                                leave='transition ease-in duration-100'
                                leaveFrom='opacity-100'
                                leaveTo='opacity-0'>
                                <Listbox.Options className='absolute z-50 mt-1 max-h-60 w-full overflow-auto rounded-md bg-popover py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm'>
                                    {options.map((option) => (
                                        <Listbox.Option
                                            key={option.id}
                                            className='relative cursor-default select-none py-1.5 pl-10 pr-4 text-sm rounded-sm outline-none focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50'
                                            value={option}>
                                            {({ selected }) => (
                                                <>
                                                    {UserService.getLastAndFirstName(option)}
                                                    {selected ? (
                                                        <span className='absolute inset-y-0 right-2 flex items-center pl-3'>
                                                            <CheckIcon className='h-4 w-4' />
                                                        </span>
                                                    ) : null}
                                                </>
                                            )}
                                        </Listbox.Option>
                                    ))}
                                </Listbox.Options>
                            </Transition>
                        </div>
                    </Listbox>
                    <FormMessage />
                </FormItem>
            )}
        />
    );
};

export default MultyRangerCombobox;