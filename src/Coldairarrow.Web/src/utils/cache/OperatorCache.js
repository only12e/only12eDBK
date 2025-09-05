import { Axios } from "@/utils/plugin/axios-plugin"

let permissions = []
let inited = false

let OperatorCache = {
    info: {},
    inited() {
        return inited
    },
    init(callBack) {
        if (inited)
            callBack()
        else {
            Axios.post('/Base_Manage/Home/GetOperatorInfo').then(resJson => {
                if (resJson && resJson.Data) {
                    this.info = resJson.Data.UserInfo || {}
                    permissions = resJson.Data.Permissions || []
                    inited = true
                }
                callBack()
            }).catch(error => {
                console.warn('获取用户信息失败:', error)
                this.info = {}
                permissions = []
                callBack()
            })
        }
    },
    hasPermission(thePermission) {
        return permissions.includes(thePermission)
    },
    clear() {
        inited = false
        permissions = []
        this.info = {}
    }
}

export default OperatorCache