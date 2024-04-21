import React, {useState} from 'react';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {User} from '@/entities/user/user.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Dialog, DialogContent, DialogTrigger} from '@/shared/ui/dialog.tsx';
import AddUserDialog from '@/features/form/add-user-dialog.tsx';
import UpdateUserDialog from '@/features/form/update-user-dialog.tsx';

interface IProps
{
    users: User[],
    increaseCount: ()=>void
}

const ManagingForm = ({users, increaseCount} : IProps) => {
    const { id } = useAppSelector(selectAuth);
    const [isOpen, setIsOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState<User>({} as User);

    return (
        <div className="font-roboto top-0 w-full flex flex-col items-center pt-8">
            <div className="w-full max-w-4xl">
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead>ID</TableHead>
                            <TableHead>Имя</TableHead>
                            <TableHead>Фамилия</TableHead>
                            <TableHead>Отчество</TableHead>
                            <TableHead>Эл. почта</TableHead>
                            <TableHead>Роли</TableHead>
                            <TableHead>Действия</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {users.map((user) => (
                            user.id !== id &&
                            <TableRow key={user.id}>
                                <TableCell>{user.id}</TableCell>
                                <TableCell>{user.lastName}</TableCell>
                                <TableCell>{user.firstName}</TableCell>
                                <TableCell>{user.middleName}</TableCell>
                                <TableCell>{user.email}</TableCell>
                                <TableCell>{user.roleNames?.join(", ")}</TableCell>
                                <TableCell>
                                    <Button
                                        variant="ghost"
                                        onClick={() =>
                                        {
                                            setSelectedUser(user)
                                            setIsOpen(true)}
                                        }
                                    >
                                        Редактировать
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
                <UpdateUserDialog user={selectedUser}
                                  isOpen={isOpen}
                                  setIsOpen={setIsOpen}/>
            </div>
        </div>
    );
}

export default ManagingForm;