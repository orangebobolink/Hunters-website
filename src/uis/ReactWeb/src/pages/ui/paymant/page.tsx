import React, {useEffect, useState} from 'react';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {HuntingLicenseService} from '@/entities/huntinLicense/HuntingLicenseService.ts';
import {HuntingLicense} from '@/entities/huntinLicense/HuntingLicense.ts';
import {toast} from '@/shared/ui/use-toast.ts';

const PaymantPage = () => {
    const {id, isPaid} = useAppSelector(selectAuth);
    const [huntingLicense, setHuntingLicense] = useState<HuntingLicense>();

    useEffect(() => {
        const fetchPermissions = async () => {
            const response = await HuntingLicenseService.getByUserId(id!);

            if (response.data) {
                toast({
                    variant: "destructive",
                    title: "Лицензия охотники отсуствует",
                    description: "На данный момент вы не обладаете подтвержденной лицензией охотника. " +
                        "Перейдите в раздел 'Лицензия охотника'"
                })
            }

            setHuntingLicense(response.data)
        }

        fetchPermissions();
    }, [isPaid]);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            <p>ds</p>
        </div>
    );
};

export default PaymantPage;