import React from 'react';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {Feeding} from '@/entities/feeding/Feeding.ts';

interface IProps
{
    feedings: Feeding[];
}

const FeedingTable = ({feedings}:IProps) => {
    return (
        <Table className="justify-center">
            <TableHeader>
                <TableRow>
                    <TableHead>Номер</TableHead>
                    <TableHead>Дата подкормки</TableHead>
                    <TableHead>Локация</TableHead>
                    <TableHead>Статус</TableHead>
                    <TableHead>Действия</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {feedings.map((feeding) => (
                    <TableRow key={feeding.id}>
                        <TableCell>
                            {feeding.number}
                        </TableCell>
                        <TableCell>
                            {feeding.feedingDate.toString()}
                        </TableCell>
                        <TableCell>
                            {feeding.land.name}
                        </TableCell>
                        <TableCell>
                            {feeding.status.toString()}
                        </TableCell>
                        <TableCell>
                            <Button
                                variant="ghost"
                                onClick={() => {

                                    //setIsOpen(true)
                                }}
                            >
                                Подробнее
                            </Button>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
};

export default FeedingTable;