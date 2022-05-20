from flask import Response
from flask_restful import Resource
from firebase_admin import auth

class TokenResource(Resource):
    def get(self):
        custom_token=auth.create_custom_token("password123");
        return Response(custom_token, mimetype="text/plain",direct_passthrough=True)