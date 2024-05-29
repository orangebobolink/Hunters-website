import { useEffect, useState } from 'react';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { useTranslation } from 'react-i18next';
import { Permission } from '@/entities/permision/Permision.ts';
import { PermissionService } from '@/entities/permision/PermissionService.ts';
import { Dialog, DialogContent, DialogTrigger } from '@/shared/ui/dialog.tsx';
import { Button } from '@/shared/ui';
import CreatePermissionForm from '@/entities/permision/ui/create-permission-form.tsx';
import PermissionDataTable from '@/features/table/PermissionDataTable';

const PermissionPage = () => {
    const [permissions, setPermissions] = useState<Permission[]>([]);
    const [isOpen, setIsOpen] = useState(false);
    const { roles, id } = useAppSelector(selectAuth);
    const { t } = useTranslation('translation', {
        keyPrefix: 'feeding',
    });

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await PermissionService.getAll();
                if (roles.includes('Ranger')) {
                    response.data = response.data.filter(
                        (f) => f.receivedId == id
                    );
                }

                setPermissions(response.data);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [isOpen]);

    return (
        <div className='select-none h-full w-full flex items-center flex-col justify-center space-y-5'>
            <div className='w-2/3 flex justify-center'>
                <PermissionDataTable permissions={permissions} />
            </div>
            <Dialog open={isOpen}>
                <DialogTrigger asChild>
                    <Button onClick={() => setIsOpen(true)}>
                        Создать разрешение
                    </Button>
                </DialogTrigger>
                <DialogContent>
                    <CreatePermissionForm setIsOpen={setIsOpen} />
                </DialogContent>
            </Dialog>
        </div>
    );
};

export default PermissionPage;
