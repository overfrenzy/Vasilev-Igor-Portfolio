import styles from '../styles/index.module.css';
import fsPromises from 'fs/promises';
import path from 'path';
import Container from '@mui/material/Container';
import AppBar from '../components/AppBar2';
import { Grid, Paper, Typography, Box } from '@mui/material';

export default function Home(props) {
	const students = props.students;
	return (
		<div className={styles.content}>
			<AppBar />
			<Container sx={{ marginY: 3 }}>
				<h1>Участники</h1>
				<Grid container spacing={6}>
					{students.map((elem) => (
						<Grid item xs={3}>
							<Paper elevation={3}>
								<img
									src={elem.image}
									alt=""
									className={styles.img}
								/>
								<Box paddingX={1}>
									<Typography variant="subtitle1" component="h2">
										{elem.participant_ru}
									</Typography>
									<Box
										sx={{
											display: 'flex',
											alignItems: 'Center',
										}}
									>
										<Typography variant="body2" component="p" marginLeft={0.5}>
											{elem.country_ru}
										</Typography>
									</Box>
									<Box
										sx={{
											display: 'flex',
											alignItems: 'Center',
										}}
									>
										<Typography variant="body2" component="p" marginLeft={0.5}>
											{elem.place}
										</Typography>

									</Box>
								</Box>
							</Paper>
						</Grid >
					))}
				</Grid>
			</Container>
		</div >
	);
}

export async function getStaticProps() {
	const filePath = path.join(process.cwd(), './shared/data2018.json');
	const jsonData = await fsPromises.readFile(filePath);
	const objectData = JSON.parse(jsonData.toString());

	return {
		props: objectData
	};
}