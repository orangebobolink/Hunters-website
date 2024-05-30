import { useEffect, useState } from 'react';
import { ProductService } from '@/entities/rent/api/ProductService.ts';
import { Product } from '@/entities/rent/models/Product.ts';
import { ScrollArea } from '@/shared/ui/scroll-area.tsx';
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
} from '@/shared/ui/command.tsx';
import MultyProductTypeCombobox from '@/features/combobox/multy-product-type-combobox.tsx';
import { Type } from '@/entities/rent/models/Type.ts';
import { useTranslation } from 'react-i18next';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { Dialog, DialogContent, DialogTrigger } from '@/shared/ui/dialog.tsx';
import { Button } from '@/shared/ui';
import { PlusIcon } from '@radix-ui/react-icons';
import RentForm from '@/entities/rent/ui/rent-form.tsx';
import { toast } from '@/shared/ui/use-toast.ts';
import { RentProduct } from '@/entities/rent/models/RentProduct.ts';
import { RentProductService } from '@/entities/rent/api/RentProductService.ts';
import ManagerView from '@/widgets/rentProduct/manager-view.tsx';
import { RentStatus } from '@/entities/rent/models/RentStatus';
import {
    Select,
    SelectContent,
    SelectGroup,
    SelectItem,
    SelectLabel,
    SelectTrigger,
    SelectValue,
} from '@/shared/ui/select';

const RentPage = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [userProducts, setUserProducts] = useState<RentProduct[]>([]);
    const [changeRender, setChangeRender] = useState<boolean>(false);
    const [filterType, setFilterType] = useState<string>('all');
    const [isOpen, setIsOpen] = useState(false);
    const [isUpdateOpen, setIsUpdateOpen] = useState(false);
    const { roles, id } = useAppSelector(selectAuth);
    const { t } = useTranslation('translation', {
        keyPrefix: 'rent',
    });
    const [selectedProduct, setProduct] = useState<Product>();

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await ProductService.getAll();
                setProducts(response.data);

                if (roles.includes('User')) {
                    const userRentProduts =
                        await RentProductService.getByUserId(id!);
                    setUserProducts(userRentProduts.data);
                }
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchProducts();
    }, [changeRender]);

    const handleDelete = async (id: string) => {
        try {
            const data = await ProductService.delete(id);

            if (data.status >= 200 && data.status <= 300) {
                toast({
                    variant: 'success',
                    title: 'Продукт удален успешно',
                });
                setProducts(products.filter((p) => p.id != id));
            }
        } catch {
            toast({
                variant: 'destructive',
                title: 'Что-то пошло не так',
            });
        }
    };

    const handleRent = async (product: Product) => {
        try {
            const rentProduct: RentProduct = {
                userId: id!,
                productId: product.id!,
            };

            const data = await RentProductService.create(rentProduct);

            if (data.status >= 200 && data.status <= 300) {
                toast({
                    variant: 'success',
                    title: 'Продукт арендован успешно',
                });

                setChangeRender(!changeRender);
            }
        } catch {
            toast({
                variant: 'destructive',
                title: 'Что-то пошло не так',
            });
        }
    };

    const options = [
        { key: Type[Type.Car], value: 'Машина' },
        { key: Type[Type.Ammunition], value: 'Амуниция' },
        { key: Type[Type.Gun], value: 'Оружие' },
        { key: 'all', value: 'Все' },
    ];

    return (
        <div className='flex flex-row w-full mt-8'>
            {roles.includes('Manager') ? (
                <ManagerView />
            ) : (
                <>
                    <div className='w-1/5 mx-5'>
                        <Command>
                            <CommandInput placeholder='Поиск по фильтрам...' />
                            <CommandList>
                                <CommandEmpty>{t('noResult')}</CommandEmpty>
                                <CommandGroup heading='Фильтры'>
                                    <CommandItem className='flex flex-col'>
                                        <div>
                                            <Select
                                                onValueChange={(
                                                    value: string
                                                ) => setFilterType(value)}
                                            >
                                                <SelectTrigger className='w-[180px]'>
                                                    <SelectValue placeholder='Тип продукта' />
                                                </SelectTrigger>
                                                <SelectContent>
                                                    <SelectGroup>
                                                        <SelectLabel>
                                                            {t('typeOfProduct')}
                                                        </SelectLabel>
                                                        {options.map(
                                                            (option) => (
                                                                <SelectItem
                                                                    value={
                                                                        option.key
                                                                    }
                                                                >
                                                                    {
                                                                        option.value
                                                                    }
                                                                </SelectItem>
                                                            )
                                                        )}
                                                    </SelectGroup>
                                                </SelectContent>
                                            </Select>
                                        </div>
                                    </CommandItem>
                                </CommandGroup>
                            </CommandList>
                        </Command>
                    </div>
                    <div className='flex gap-4 mx-5'>
                        <ScrollArea className='h-[85dvh] w-[75dvw] rounded-md border'>
                            <div className='flex flex-wrap flex-row p-4'>
                                {products.map((product) => {
                                    if (
                                        filterType == 'all' ||
                                        filterType == Type[product.type]
                                    ) {
                                        return (
                                            <div
                                                key={product.id}
                                                className='border rounded-lg p-4 m-5'
                                            >
                                                <img
                                                    src={product.imageUrl}
                                                    alt={`Image of ${product.name}`}
                                                    className='h-[200px] w-full object-cover'
                                                />
                                                <div className='flex flex-col'>
                                                    <h3 className='font-semibold text-lg mt-2'>
                                                        {product.name}
                                                    </h3>
                                                    <p className='text-sm text-[#555]'>
                                                        {t('price')}:{' '}
                                                        {product.price} бел. руб
                                                    </p>
                                                    <p className='text-sm text-[#555]'>
                                                        {t('onStock')}:{' '}
                                                        {
                                                            product.quantityInStock
                                                        }
                                                    </p>
                                                </div>
                                                {userProducts.some(
                                                    (userProduct) =>
                                                        userProduct.productId ===
                                                            product.id &&
                                                        (userProduct.status ==
                                                            RentStatus[
                                                                RentStatus
                                                                    .Pending
                                                            ] ||
                                                            userProduct.status ==
                                                                RentStatus[
                                                                    RentStatus
                                                                        .Rented
                                                                ])
                                                ) ? (
                                                    userProducts.some(
                                                        (userProduct) =>
                                                            userProduct.productId ===
                                                                product.id &&
                                                            userProduct.status ==
                                                                RentStatus[
                                                                    RentStatus
                                                                        .Pending
                                                                ]
                                                    ) ? (
                                                        <div>
                                                            Ваша заявка на
                                                            аренду
                                                            рассматривается
                                                        </div>
                                                    ) : (
                                                        <div>
                                                            Вы уже арендуете
                                                            данный товар
                                                        </div>
                                                    )
                                                ) : (
                                                    roles.includes('User') &&
                                                    product.quantityInStock >
                                                        0 && (
                                                        <Button
                                                            className='m-3'
                                                            onClick={() => {
                                                                handleRent(
                                                                    product
                                                                );
                                                            }}
                                                        >
                                                            {t('rent')}
                                                        </Button>
                                                    )
                                                )}
                                                {roles.includes('Admin') && (
                                                    <>
                                                        <Button
                                                            className='m-3'
                                                            onClick={() => {
                                                                console.log(
                                                                    product
                                                                );
                                                                setProduct(
                                                                    product
                                                                );
                                                                setIsUpdateOpen(
                                                                    true
                                                                );
                                                            }}
                                                        >
                                                            {t('update')}
                                                        </Button>
                                                        <Button
                                                            className='m-3'
                                                            onClick={() => {
                                                                handleDelete(
                                                                    product.id!
                                                                );
                                                            }}
                                                        >
                                                            {t('delete')}
                                                        </Button>
                                                    </>
                                                )}
                                            </div>
                                        );
                                    }
                                })}

                                {roles.includes('Admin') && (
                                    <>
                                        <Dialog
                                            onOpenChange={() =>
                                                setIsOpen(false)
                                            }
                                        >
                                            <DialogTrigger asChild>
                                                <Button
                                                    onClick={() =>
                                                        setIsOpen(true)
                                                    }
                                                    variant='outline'
                                                >
                                                    <PlusIcon />
                                                </Button>
                                            </DialogTrigger>
                                            <DialogContent>
                                                <RentForm
                                                    selectedProduct={null}
                                                    changeRender={changeRender}
                                                    setChangeRender={
                                                        setChangeRender
                                                    }
                                                />
                                            </DialogContent>
                                        </Dialog>
                                        <Dialog
                                            open={isUpdateOpen}
                                            onOpenChange={() =>
                                                setIsUpdateOpen(false)
                                            }
                                        >
                                            <DialogContent>
                                                <RentForm
                                                    selectedProduct={
                                                        selectedProduct!
                                                    }
                                                    type='update'
                                                    changeRender={changeRender}
                                                    setChangeRender={
                                                        setChangeRender
                                                    }
                                                />
                                            </DialogContent>
                                        </Dialog>
                                    </>
                                )}
                            </div>
                        </ScrollArea>
                    </div>
                </>
            )}
        </div>
    );
};

export default RentPage;
