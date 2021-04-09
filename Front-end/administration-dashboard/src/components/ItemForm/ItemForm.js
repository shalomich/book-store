import React, {Component} from 'react';
import {withRouter} from "react-router-dom";
import axios from "axios";
import {createForm} from "./CreateForm";
import {getFormData} from "./GetFormData";
import {setFormData} from "./SetFormData";

class ItemForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            formData: {},
            config: {},
            form: {},
            formToEdit: {},
            sendClicked: false
        }

        this.images =
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
        if (this.props.action === 'edit') {
            axios.get("https://localhost:44327/storage/publication/" + sessionStorage.getItem('editItemId'))
                .then(res => {
                    this.setState({formToEdit: res.data})
                })
        }
    }

    componentDidUpdate(prevProps, prevState, snapshot) {
        if ((prevState !== this.state) && (this.state.sendClicked)) {
            if (this.props.action !== 'edit') {
                console.log("post")
                axios.post("https://localhost:44327/storage/publication", this.state.form, { headers: {'Content-Type': 'application/json' }})
                    .then(res => {
                        console.log(res.status)
                    })
            }
            else {
                console.log(this.state.form)
                axios.put("https://localhost:44327/storage/publication/" + sessionStorage.getItem('editItemId'), this.state.form, { headers: {'Content-Type': 'application/json' }})
                    .then(res => {
                        console.log(res.status)
                    })
            }

            this.props.history.push("/admin/" + this.props.type)
        }
    }

    handleClickBack = (e) => {
        e.preventDefault();
        this.props.history.push("/admin/" + this.props.type)
    }

    handleClickSend = (e) => {
        e.preventDefault();
        let form = getFormData(this.ref.current.children)
        if (form.images.length !== 0) {
            if (this.props.action === 'edit') {
                form.id = sessionStorage.getItem('editItemId')
            }
            Promise.allSettled(form.images)
                .then((results) => {
                    let arr = []
                    results.forEach(result => {
                        arr.push(result.value)
                    })
                    form.images = arr
                    this.setState({form: form, sendClicked: true})
                    console.log(this.state.form)
                })
        } else {
            form.images = this.state.formToEdit.images
            form.id = sessionStorage.getItem('editItemId')
            this.setState({form: form, sendClicked: true})
        }

    }

    render() {
        console.log(this.props.action)
        if (this.props.action === 'edit') {
            this.info = setFormData(this.state.formData, this.state.config, this.state.formToEdit)
        }
        else {
            this.info = createForm(this.state.formData, this.state.config);
        }
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