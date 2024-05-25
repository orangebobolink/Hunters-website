import { useEffect, useState } from 'react';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { HuntingLicenseService } from '@/entities/huntinLicense/HuntingLicenseService.ts';
import { HuntingLicense } from '@/entities/huntinLicense/HuntingLicense.ts';
import { toast } from '@/shared/ui/use-toast.ts';
import { Button } from '@/shared/ui';
import { useActions } from '@/shared/lib/hooks/useActions';

const PaymantPage = () => {
    const { id, isPaid } = useAppSelector(selectAuth);
    const [huntingLicense, setHuntingLicense] = useState<HuntingLicense>();
    const [changeRender, setChangeRender] = useState<boolean>();
    const { changeIsPaid } = useActions();

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await HuntingLicenseService.getByUserId(id!);
                console.log(response.data);
                if (response.status > 400) {
                    toast({
                        variant: 'destructive',
                        title: 'Лицензия охотники отсуствует',
                        description:
                            'На данный момент вы не обладаете подтвержденной лицензией охотника. ' +
                            "Перейдите в раздел 'Лицензия охотника'",
                    });
                }

                setHuntingLicense(response.data);
            } catch {
                toast({
                    variant: 'destructive',
                    title: 'Лицензия охотники отсуствует',
                    description:
                        'На данный момент вы не обладаете подтвержденной лицензией охотника. ' +
                        "Перейдите в раздел 'Лицензия охотника'",
                });
            }
        };

        fetchPermissions();
    }, [changeRender]);

    const handlerPayment = async () => {
        try {
            const response = await HuntingLicenseService.payFee(
                huntingLicense?.licenseNumber!
            );

            if (response.data) {
                toast({
                    variant: 'success',
                    title: 'Оплата прошла успешно',
                });

                setHuntingLicense({ ...huntingLicense, isPaid: true });
                setChangeRender(!changeRender);
                changeIsPaid({ flag: true });
            } else {
                toast({
                    variant: 'destructive',
                    title: 'Оплата прошла не успешно',
                });
            }
        } catch {
            toast({
                variant: 'destructive',
                title: 'Оплата прошла не успешно',
            });
        }
    };

    return (
        <div className='select-none h-full w-full flex items-center flex-col justify-center space-y-5'>
            {huntingLicense ? (
                <div>
                    <p>Номер лицензии:{huntingLicense.licenseNumber}</p>
                    {huntingLicense.isPaid ? (
                        <div>Оплачена</div>
                    ) : (
                        <Button onClick={handlerPayment}>Оплатить</Button>
                    )}
                </div>
            ) : (
                <div>Лицензии охотника не обнаруженно</div>
            )}
        </div>
    );
};

export default PaymantPage;
