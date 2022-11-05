import {FC, ReactElement, useRef, useEffect, useState} from 'react'
import {CreateJobCommandDto, Client, JobLookupDto} from '../api/api';
import {FormControl} from 'react-bootstrap';

const apiClient = new Client('https://localhost:7250')

async function createJob(job:CreateJobCommandDto) {
    await apiClient.create('1.0', job);
    console.log('Job was created.');
}

// const JobList: FC<{}> = (): ReactElement => {
//     let textInput = useRef(null);
//     const [jobs, setJobs] = useState<JobLookupDto[] | undefined>(undefined);

//     async function getJobs() {
//         const jobListVm = await apiClient.getAll('1.0');
//         setJobs(jobListVm.jobs);
//     }

//     useEffect(() => {
//         getJobs();
//     }, []);
// }