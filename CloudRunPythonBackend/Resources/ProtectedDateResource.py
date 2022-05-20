from flask import jsonify
from flask_restful import Resource
from http import HTTPStatus
from datetime import datetime

class ProtectedDateResource(Resource):
    def get(selfs):
        today=datetime.now()
        tstr=today.strftime('%m/%d/%Y %H:%m')
        return tstr,HTTPStatus.OK