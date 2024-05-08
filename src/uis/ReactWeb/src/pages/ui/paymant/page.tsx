import React, {useEffect, useState} from 'react';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {HuntingLicenseService} from '@/entities/huntinLicense/HuntingLicenseService.ts';
import {HuntingLicense} from '@/entities/huntinLicense/HuntingLicense.ts';
import {toast} from '@/shared/ui/use-toast.ts';
import {Button} from '@/shared/ui';

const PaymantPage = () => {
    const {id, isPaid} = useAppSelector(selectAuth);
    const [huntingLicense, setHuntingLicense] = useState<HuntingLicense>();

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await HuntingLicenseService.getByUserId(id!);
                console.log("sdsd")
                if (response.status > 400) {
                    toast({
                        variant: "destructive",
                        title: "Лицензия охотники отсуствует",
                        description: "На данный момент вы не обладаете подтвержденной лицензией охотника. " +
                            "Перейдите в раздел 'Лицензия охотника'"
                    })
                }

                setHuntingLicense(response.data)
            }
            catch
            {
                toast({
                    variant: "destructive",
                    title: "Лицензия охотники отсуствует",
                    description: "На данный момент вы не обладаете подтвержденной лицензией охотника. " +
                        "Перейдите в раздел 'Лицензия охотника'"
                })
            }
        }

        fetchPermissions();
    }, [isPaid]);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            {
                huntingLicense
                ?
                <div>
                    <p>Номер лицензии:{huntingLicense.licenseNumber}</p>
                    <Button>Оплатить</Button>
                </div>
                : <div>Лицензии охотника не обнаруженно</div>
            }
        </div>
    );
};

export default PaymantPage;