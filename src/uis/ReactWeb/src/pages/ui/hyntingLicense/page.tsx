import { useEffect, useState } from 'react';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { HuntingLicense } from '@/entities/huntinLicense/HuntingLicense.ts';
import { HuntingLicenseService } from '@/entities/huntinLicense/HuntingLicenseService.ts';
import CreateHuntingLicenseForm from '@/entities/huntinLicense/ui/create-huntingLicense-form.tsx';

const HuntingLicensePage = () => {
    const { id, isPaid } = useAppSelector(selectAuth);
    const [huntingLicense, setHuntingLicense] = useState<HuntingLicense>();
    const [changeRender, setChangeRender] = useState<boolean>();

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await HuntingLicenseService.getByUserId(id!);

                setHuntingLicense(response.data);
            } catch {}
        };

        fetchPermissions();
    }, [isPaid, changeRender]);

    return (
        <div className='select-none w-full flex items-center flex-col justify-center space-y-10'>
            {huntingLicense ? (
                <div>
                    <p>Номер лицензии: {huntingLicense.licenseNumber}</p>
                </div>
            ) : (
                <div>
                    <CreateHuntingLicenseForm
                        setChangeRender={setChangeRender}
                    />
                </div>
            )}
        </div>
    );
};

export default HuntingLicensePage;
