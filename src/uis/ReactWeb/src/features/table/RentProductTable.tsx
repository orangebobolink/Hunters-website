import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {format} from 'date-fns';
import {Button} from '@/shared/ui';
import {RentProduct} from '@/entities/rent/models/RentProduct.ts';
import {UserService} from '@/entities/user/api/UserService.ts';
import {useTranslation} from 'react-i18next';
import {RentStatus} from '@/entities/rent/models/RentStatus.ts';
import {RentProductService} from '@/entities/rent/api/RentProductService.ts';
import {toast} from '@/shared/ui/use-toast.ts';

interface IProps
{
    rentProducts: RentProduct[];
    setRentProducts: (rentProducts:RentProduct[]) => void;
}

const RentProductTable = ({rentProducts, setRentProducts}:IProps) => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.table"
        });

    const handle = async (rentProduct:RentProduct) => {
        const data = await RentProductService.update(rentProduct);

        if (data.status >= 200 && data.status <= 300) {
            toast({
                variant: "success",
                title: "Продукт удален успешно",
            })

            const newRentProducts = rentProducts.map(oldRentProduct => {
                if (oldRentProduct.id === rentProduct.id) {
                    return rentProduct;
                }
                return oldRentProduct;
            })
            console.log(newRentProducts)
            setRentProducts(newRentProducts);
        }
    }

    const handleRented = async (rentProduct:RentProduct) => {
        rentProduct.status = RentStatus[RentStatus.Rented];
        await handle(rentProduct)
    }

    const handleCancelled = async (rentProduct:RentProduct) => {
        rentProduct.status = RentStatus[RentStatus.Cancelled];
        await handle(rentProduct)
    }

    const handleReturned = async (rentProduct:RentProduct) => {
        rentProduct.status = RentStatus[RentStatus.Returned];
        await handle(rentProduct)
    }

    const content = (rentProduct:RentProduct) => (
        <TableRow key={rentProduct.id}>
            <TableCell>
                {UserService.getFullName(rentProduct.user)}
            </TableCell>
            <TableCell>
                {rentProduct.product?.name}
            </TableCell>
            <TableCell>
                {format(rentProduct.fromDate!, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {format(rentProduct.toDate!, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {rentProduct.status}
            </TableCell>
            <TableCell>
                {
                    rentProduct.status == RentStatus[RentStatus.Pending]
                    &&
                    <>
                        <Button
                            variant="ghost"
                            onClick={async () => {
                                await handleRented(rentProduct)
                            }}
                        >
                            Подтвердить
                        </Button>
                        <Button
                            variant="ghost"
                            onClick={async () => {
                                await handleCancelled(rentProduct)
                            }}
                        >
                            Отказать
                        </Button>
                    </>
                }
                {
                    rentProduct.status == RentStatus[RentStatus.Rented]
                    &&
                    <Button
                        variant="ghost"
                        onClick={async () => {
                            await handleReturned(rentProduct)
                        }}
                    >
                        Подтвердить возврат
                    </Button>
                }
            </TableCell>
        </TableRow>
    )

    return (
        <>
            <Table className="justify-center">
                <TableHeader>
                    <TableRow>
                        <TableHead>{t("fullName")}</TableHead>
                        <TableHead>{t("productName")}</TableHead>
                        <TableHead>{t("fromDate")}</TableHead>
                        <TableHead>{t("toDate")}</TableHead>
                        <TableHead>{t("status")}</TableHead>
                        <TableHead> </TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {rentProducts?.map((rentProduct) => content(rentProduct) )}
                </TableBody>
            </Table>
        </>
    );
};

export default RentProductTable;