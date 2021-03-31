import React, {Component} from 'react';
import {withRouter} from "react-router-dom";
import axios from "axios";
import {createForm} from "./FormCreation";
import {getFormData} from "./GetFormData";

class ItemForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            formData: {},
            config: {}
        }
        this.ref = React.createRef()
    }

    componentDidMount() {
        axios.get("https://localhost:44327/storage/publication/form")
            .then(res => {
                this.setState({formData: res.data})

            })
        axios.get("https://localhost:44327/storage/publication/config")
            .then(res => {
                this.setState({config: res.data})
                console.log(this.state.config)
            })
    }

    handleClickBack = (e) => {
        e.preventDefault();
        this.props.history.push("/admin/" + this.props.type)
    }

    handleClickSend = (e) => {
        e.preventDefault();

        let form = getFormData(this.ref.current.children)
        console.log(JSON.stringify(form))
        // axios.post("https://localhost:44327/storage/publication", form, { headers: {'Content-Type': 'application/json' }})
        //     .then(res => {
        //          console.log(res.status)
        //      })
        this.props.history.push("/admin/" + this.props.type)
    }

    render() {
        console.log(this.state.formData)

        this.info = createForm(this.state.formData, this.state.config);
        return (
            <div>
                <button onClick={this.handleClickBack}>Назад</button>
                <form ref={this.ref}>
                    {this.info}
                    <button onClick={this.handleClickSend}>Завершить</button>
                </form>
            </div>
        )
    }
}

export default withRouter(ItemForm);