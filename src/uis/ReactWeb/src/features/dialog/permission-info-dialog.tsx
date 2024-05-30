import { Permission } from '@/entities/permision/Permision.ts';
import { Dialog, DialogContent } from '@/shared/ui/dialog.tsx';
import { format } from 'date-fns';
import { UserService } from '@/entities/user/api/UserService.ts';
import { Button } from '@/shared/ui';
import { FeedingService } from '@/entities/feeding/api/FeedingService.ts';
import { toast } from '@/shared/ui/use-toast.ts';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { PermissionService } from '@/entities/permision/PermissionService.ts';
import { Status } from '@/entities/status/Status.ts';
import { AxiosResponse } from 'axios';
import { EnumService } from '../statusTranslate';

interface IProps {
    permission: Permission;
    isOpen: boolean;
    setIsOpen: (flag: boolean) => void;
}

const PermissionInfoDialog = ({ permission, isOpen, setIsOpen }: IProps) => {
    const { roles } = useAppSelector(selectAuth);

    const handlerDelete = async () => {
        const response = await PermissionService.delete(permission.id!);

        if (response.status >= 200 && response.status <= 300) {
            toast({
                variant: 'success',
                title: 'Разрешение удаленно успешно',
            });
            setIsOpen(false);
        } else {
            toast({
                variant: 'destructive',
                title: 'Что-то пошло не так',
            });
            setIsOpen(false);
        }
    };

    const handler = async (response: AxiosResponse<any>) => {
        if (response.status >= 200 && response.status <= 300) {
            toast({
                variant: 'success',
                title: 'Статус успешно изменен',
            });
            setIsOpen(false);
        } else {
            toast({
                variant: 'destructive',
                title: 'Что-то пошло не так',
            });
            setIsOpen(false);
        }
    };

    const handlerReceived = async () => {
        permission.status = Status[Status.Recived];
        const response = await PermissionService.update(permission);

        handler(response);
    };

    return (
        <Dialog open={isOpen} onOpenChange={() => setIsOpen(false)}>
            <DialogContent>
                <div className='p-4 shadow-md rounded-lg'>
                    <p className='font-bold'>
                        Номер:{' '}
                        <span className='font-normal'>{permission.number}</span>
                    </p>
                    <p className='font-bold'>
                        Дата подкормки:{' '}
                        <span className='font-normal'>
                            {format(permission.fromDate, 'MM/dd/yyyy')}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Дата подкормки:{' '}
                        <span className='font-normal'>
                            {format(permission.toDate, 'MM/dd/yyyy')}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Составитель:{' '}
                        <span className='font-normal'>
                            {UserService.getFullName(permission.issued!)}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Выданно егерю:{' '}
                        <span className='font-normal'>
                            {UserService.getFullName(permission.received!)}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Состояние:{' '}
                        <span className='font-normal'>
                            {EnumService.statysTranslate(permission.status)}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Локация:{' '}
                        <span className='font-normal'>
                            {permission.land?.name}
                        </span>
                    </p>
                    <p className='font-bold'>
                        Имя животного:{' '}
                        <span className='font-normal'>
                            {permission.animal?.name}
                        </span>
                    </p>
                </div>
                {roles.includes('Manager') && (
                    <Button onClick={handlerDelete}>Удалить</Button>
                )}
                {roles.includes('Ranger') &&
                    permission.status == Status[Status.Given] && (
                        <Button onClick={handlerReceived}>Принять</Button>
                    )}
            </DialogContent>
        </Dialog>
    );
};

export default PermissionInfoDialog;
