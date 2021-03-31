import axios from "axios";

export const getData = () => {
    axios.get("https://localhost:44327/storage/publication")
        .then(res => {
            if(res.status===200){
                console.log(res.data)
                return res.data;
            }
    });
}
