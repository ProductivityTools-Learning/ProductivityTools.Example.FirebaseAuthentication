from flask import request
from flask_restful import Resource
from firebase_admin import auth
from http import HTTPStatus
from datetime import datetime

class ProtectedDateResource(Resource):
    def get(selfs):
        id_token=request.headers.environ["HTTP_AUTHORIZATION"]
        id_token=id_token.replace("Bearer","")
        id_token=id_token.replace(" ","")
        decoded_token=auth.verify_id_token(id_token)
        today=datetime.now()
        tstr=today.strftime('%m/%d/%Y %H:%m:%S')
        return tstr,HTTPStatus.OK