import * as React from 'react';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import fsPromises from 'fs/promises';
import path from 'path';
import styles from "../styles/tables.module.css";
import AppBar from '../components/AppBar1';
import Container from '@mui/material/Container';


export async function getStaticProps() {
	const filePath = path.join(process.cwd(), './shared/data2021.json');
	const jsonData = await fsPromises.readFile(filePath);
	const objectData = JSON.parse(jsonData.toString());

	return {
		props: objectData
	};
}

interface Column {
	id: '№' | 'Name' | 'Country' | 'One1' | 'One2' | 'One3' | 'TotalOne' | 'Second1' | 'Second2' | 'Second3' | 'TotalSecond' | 'Total' | 'Place';
	label: string;
	minWidth?: number;
	align?: 'right';
	format?: (value: number) => string;
}

const columns: readonly Column[] = [
	{ id: '№', label: '№', minWidth: 1 },
	{ id: 'Name', label: 'ФИО Участника', minWidth: 100 },
	{ id: 'Country', label: 'Страна', minWidth: 50 },
	{ id: 'One1', label: '1', minWidth: 5, align: 'right', },
	{ id: 'One2', label: '2', minWidth: 5, align: 'right', },
	{ id: 'One3', label: '3', minWidth: 5, align: 'right', },
	{ id: 'TotalOne', label: 'Итого1', minWidth: 5, align: 'right', },
	{ id: 'Second1', label: '1', minWidth: 5, align: 'right', },
	{ id: 'Second2', label: '2', minWidth: 5, align: 'right', },
	{ id: 'Second3', label: '3', minWidth: 5, align: 'right', },
	{ id: 'TotalSecond', label: 'Итого2', minWidth: 5, align: 'right', },
	{ id: 'Total', label: 'Итого', minWidth: 5, align: 'right', },
	{ id: 'Place', label: 'Место', minWidth: 5, align: 'right', },
];

export default function StickyHeadTable(props) {
	const students = props.students;
	return (
		<>
			<AppBar />
			<Container>
				<Paper sx={{ width: '100%', overflow: 'hidden' }}>
					<TableContainer sx={{ maxHeight: 1100 }}>
						<Table stickyHeader aria-label="sticky table">
							<TableHead>
								<TableRow>
									<TableRow>
										<TableCell align="right" colSpan={6}>
											First Day Problems
										</TableCell>
										<TableCell align="center" colSpan={5}>
											Second Day Problems
										</TableCell>
									</TableRow>
									{columns.map((column) => (
										<TableCell
											key={column.id}
											align={column.align}
											style={{ top: 57, minWidth: column.minWidth }}
										>
											{column.label}
										</TableCell>
									))}
								</TableRow>
							</TableHead>
							<TableBody>
								{students.map((elem, i) => (
									<tr>
										<td>{i + 1}</td>
										<td>
											{elem.participant_ru} / {elem.participant_en}
										</td>
										<td>{elem.country_ru}</td>
										<td className={styles.value}>{elem.problems1[0]}</td>
										<td className={styles.value}>{elem.problems1[1]}</td>
										<td className={styles.value}>{elem.problems1[2]}</td>
										<td className={styles.value}>
											{elem.problems1.reduce((s, v) => (s += v), 0)}
										</td>
										<td className={styles.value}>{elem.problems2[0]}</td>
										<td className={styles.value}>{elem.problems2[1]}</td>
										<td className={styles.value}>{elem.problems2[2]}</td>
										<td className={styles.value}>
											{elem.problems2.reduce((s, v) => (s += v), 0)}
										</td>
										<td className={styles.value}>
											{elem.problems1.reduce((s, v) => (s += v), 0) +
												elem.problems2.reduce((s, v) => (s += v), 0)}
										</td>
										<td className={styles.value}>{elem.place}</td>
									</tr>
								))}
							</TableBody>
						</Table>
					</TableContainer>
				</Paper>
			</Container>
		</>
	);
}
